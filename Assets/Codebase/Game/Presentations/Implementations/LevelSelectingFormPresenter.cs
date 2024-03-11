using System;
using System.Collections.Generic;
using Codebase.Core.Common.General.Extensions.ListExtentions;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Game.CQRS.Queries;
using Codebase.Game.Services.Interfaces;
using Codebase.Game.Views.Interfaces;

namespace Codebase.Game.Presentations.Implementations
{
    public class LevelSelectingFormPresenter : IPresenter
    {
        private ILevelViewRepository _levelViewRepository;
        private ILevelSelectingFormView _levelSelectingForm;
        private ILiveData<IReadOnlyList<int>> _levelIds;

        private IReadOnlyList<int> _currentIds = Array.Empty<int>();

        public LevelSelectingFormPresenter
        (
            int id,
            GetLevelIdsQuery getLevelIdsQuery,
            ILevelViewRepository levelViewRepository,
            ILevelSelectingFormView levelSelectingForm
        )
        {
            _levelViewRepository = levelViewRepository;
            _levelSelectingForm = levelSelectingForm;
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

        private void OnLevelIdsChanged(IReadOnlyList<int> ids)
        {
            (IEnumerable<int> added, IEnumerable<int> removed) = _currentIds.Diff(ids);

            foreach (int addedId in added)
            {
                ILevelView view = _levelViewRepository.GetView(addedId);

                _levelSelectingForm.SetParent(view);
            }

            foreach (int removedId in removed)
            {
                ILevelView view = _levelViewRepository.GetView(removedId);

                view.UnParent();
            }
        }
    }
}
