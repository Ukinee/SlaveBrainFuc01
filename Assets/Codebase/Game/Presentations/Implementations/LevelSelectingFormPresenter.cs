using System;
using System.Collections.Generic;
using Codebase.Core.Common.General.Extensions.ListExtentions;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Game.CQRS.Queries;
using Codebase.Game.Presentations.Interfaces;
using Codebase.Game.Services.Interfaces;
using Codebase.Game.Views.Interfaces;

namespace Codebase.Game.Presentations.Implementations
{
    public class LevelSelectingFormPresenter : ILevelSelectingFormPresenter
    {
        private ILevelViewRepository _levelViewRepository;
        private ILevelSelectingFormView _levelSelectingForm;
        private readonly IInterfaceService _interfaceService;
        private ILiveData<IReadOnlyList<int>> _levelIds;

        private IReadOnlyList<int> _currentIds = Array.Empty<int>();

        public LevelSelectingFormPresenter
        (
            int id,
            GetLevelIdsQuery getLevelIdsQuery,
            ILevelViewRepository levelViewRepository,
            ILevelSelectingFormView levelSelectingForm,
            IInterfaceService interfaceService
        )
        {
            _levelViewRepository = levelViewRepository;
            _levelSelectingForm = levelSelectingForm;
            _interfaceService = interfaceService;
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

        public void OnBackClicked()
        {
            _interfaceService.Hide(new LevelSelectorFormType());
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
    }
}
