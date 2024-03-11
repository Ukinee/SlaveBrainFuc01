using Codebase.Core.Common.General.LiveDatas;
using Codebase.Game.CQRS.Queries;
using Codebase.Game.Presentations.Interfaces;
using Codebase.Game.Services.Interfaces;
using Codebase.Game.Views.Interfaces;

namespace Codebase.Game.Presentations.Implementations
{
    public class LevelPresenter : ILevelPresenter
    {
        private int _id;
        private ILevelView _view;
        private ISelectedLevelService _selectedLevelService;
        private ILiveData<bool> _isSelected;
        private ILiveData<bool> _isPassed;

        public LevelPresenter
        (
            int id,
            GetLevelSelectionQuery getLevelSelectionQuery,
            GetLevelStateQuery getLevelStateQuery,
            ILevelView view,
            ISelectedLevelService selectedLevelService
        )
        {
            _id = id;
            _view = view;
            _selectedLevelService = selectedLevelService;
            _isSelected = getLevelSelectionQuery.Handle(id);
            _isPassed = getLevelStateQuery.Handle(id);
        }

        public void Enable()
        {
            _isSelected.AddListener(OnIsSelectedChanged);
            _isPassed.AddListener(OnIsPassedChanged);
        }

        public void Disable()
        {
            _isSelected.RemoveListener(OnIsSelectedChanged);
            _isPassed.RemoveListener(OnIsPassedChanged);
        }

        public void OnButtonClick()
        {
            _selectedLevelService.Select(_id);
        }

        private void OnIsPassedChanged(bool isPassed)
        {
            _view.SetPassed(isPassed);
        }

        private void OnIsSelectedChanged(bool isSelected)
        {
            _view.SetSelected(isSelected);
        }
    }
}
