using System;
using System.Collections.Generic;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.General.Extensions.ListExtentions;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Game.Views.Interfaces;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Presentations.Implementations
{
    public class LevelSelectingFormPresenter : ILevelSelectingFormPresenter
    {
        private readonly int _id;
        private DisposeCommand _disposeCommand;
        private GetLevelIdQuery _getLevelIdQuery;
        private ILevelViewRepository _levelViewRepository;
        private ILevelSelectingFormView _levelSelectingForm;
        private IInterfaceService _interfaceService;
        private ISelectedLevelService _selectedLevelService;
        private IStateMachineService<IScenePayload> _sceneStateMachine;
        private ILiveData<IReadOnlyList<int>> _levelIds;

        private IReadOnlyList<int> _currentIds = Array.Empty<int>();

        public LevelSelectingFormPresenter
        (
            int id,
            DisposeCommand disposeCommand,
            GetLevelIdsQuery getLevelIdsQuery,
            GetLevelIdQuery getLevelIdQuery,
            ILevelViewRepository levelViewRepository,
            ILevelSelectingFormView levelSelectingForm,
            IInterfaceService interfaceService,
            ISelectedLevelService selectedLevelService,
            IStateMachineService<IScenePayload> sceneStateMachine
        )
        {
            _id = id;
            _disposeCommand = disposeCommand;
            _getLevelIdQuery = getLevelIdQuery;
            _levelViewRepository = levelViewRepository;
            _levelSelectingForm = levelSelectingForm;
            _interfaceService = interfaceService;
            _selectedLevelService = selectedLevelService;
            _sceneStateMachine = sceneStateMachine;
            _levelIds = getLevelIdsQuery.Handle(id);
        }

        public void Enable()
        {
            _levelIds.AddListener(OnLevelIdsChanged);
        }

        public void Disable()
        {
            _levelIds.RemoveListener(OnLevelIdsChanged);
        }

        public void OnStartClicked()
        {
            if (_selectedLevelService.CurrentId == -1)
                return;

            _sceneStateMachine.SetState
            (
                new GameplayScenePayload(_getLevelIdQuery.Handle(_selectedLevelService.CurrentId), MapType.Grass1)
            ); // todo: hardcoded value, to service ? 
        }

        public void OnBackClicked()
        {
            _interfaceService.Hide(new LevelSelectorFormType());
        }

        public void OnViewDispose()
        {
            Dispose();
        }

        private void OnLevelIdsChanged(IReadOnlyList<int> ids)
        {
            (IEnumerable<int> added, IEnumerable<int> removed) = _currentIds.Diff(ids);

            foreach (int addedId in added)
            {
                ILevelView view = _levelViewRepository.GetView(addedId);

                _levelSelectingForm.SetChild(view);
            }

            foreach (int removedId in removed)
            {
                ILevelView view = _levelViewRepository.GetView(removedId);

                view.UnParent();
            }
        }

        private void Dispose()
        {
            _disposeCommand.Handle(_id);

            _currentIds = null;
            _disposeCommand = null;
            _getLevelIdQuery = null;
            _levelViewRepository = null;
            _levelSelectingForm = null;
            _interfaceService = null;
            _selectedLevelService = null;
            _sceneStateMachine = null;
            _levelIds = null;
        }
    }
}
