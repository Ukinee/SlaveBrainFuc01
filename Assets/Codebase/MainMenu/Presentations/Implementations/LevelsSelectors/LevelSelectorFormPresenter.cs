using System;
using System.Collections.Generic;
using Codebase.Core.Common.General.Extensions.ListExtentions;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;

namespace Codebase.MainMenu.Presentations.Implementations.LevelsSelectors
{
    [Obsolete("Refactor to multiple presenters")]
    public class LevelSelectorFormPresenter : ILevelSelectingFormPresenter
    {
        private readonly int _id;
        private DisposeCommand _disposeCommand;
        private ILevelViewRepository _levelViewRepository;
        private IMapViewRepository _mapViewRepository;
        private ILevelSelectingFormView _levelSelectingForm;
        private IInterfaceService _interfaceService;
        private IMainMenuLevelChanger _mainMenuLevelChanger;

        private ILiveData<IReadOnlyList<int>> _levelIds;
        private ILiveData<IReadOnlyList<int>> _mapIds;

        private IReadOnlyList<int> _currentLevelIds = Array.Empty<int>();
        private IReadOnlyList<int> _currentMapIds = Array.Empty<int>();

        public LevelSelectorFormPresenter
        (
            int id,
            DisposeCommand disposeCommand,
            GetLevelIdsQuery getLevelIdsQuery,
            GetMapIdsQuery getMapIdsQuery,
            ILevelViewRepository levelViewRepository,
            IMapViewRepository mapViewRepository,
            ILevelSelectingFormView levelSelectingForm,
            IInterfaceService interfaceService,
            IMainMenuLevelChanger mainMenuLevelChanger
        )
        {
            _id = id;
            _disposeCommand = disposeCommand;
            _levelViewRepository = levelViewRepository;
            _mapViewRepository = mapViewRepository;
            _levelSelectingForm = levelSelectingForm;
            _interfaceService = interfaceService;
            _mainMenuLevelChanger = mainMenuLevelChanger;

            _levelIds = getLevelIdsQuery.Handle(id);
            _mapIds = getMapIdsQuery.Handle(id);
        }

        public void Enable()
        {
            _levelIds.AddListener(OnLevelIdsChanged);
            _mapIds.AddListener(OnMapIdsChanged);
        }

        public void Disable()
        {
            _levelIds.RemoveListener(OnLevelIdsChanged);
            _mapIds.RemoveListener(OnMapIdsChanged);
        }

        public void OnStartClicked() =>
            _mainMenuLevelChanger.Change();

        public void OnBackClicked() =>
            _interfaceService.Hide(new LevelSelectorFormType());

        public void OnViewDispose() =>
            Dispose();

        private void OnMapIdsChanged(IReadOnlyList<int> ids)
        {
            (IEnumerable<int> added, IEnumerable<int> _) = _currentMapIds.Diff(ids);
            
            foreach (int addedId in added)
            {
                IMainMenuMapView view = _mapViewRepository.GetView(addedId);
                _levelSelectingForm.AddMap(view);
            }
        }

        private void OnLevelIdsChanged(IReadOnlyList<int> ids)
        {
            (IEnumerable<int> added, IEnumerable<int> _) = _currentLevelIds.Diff(ids);

            foreach (int addedId in added)
            {
                ILevelView view = _levelViewRepository.GetView(addedId);
                _levelSelectingForm.AddLevel(view);
            }
        }
        
        private void Dispose()
        {
            _disposeCommand.Handle(_id);

            _currentLevelIds = null;
            _disposeCommand = null;
            _levelViewRepository = null;
            _levelSelectingForm = null;
            _interfaceService = null;
            _levelIds = null;
            _currentMapIds = null;
        }
    }
}
