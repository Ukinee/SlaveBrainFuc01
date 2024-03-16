using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Game.CQRS.Queries;
using Codebase.Game.Views.Interfaces;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;

namespace Codebase.MainMenu.Presentations.Implementations
{
    public class LevelPresenter : ILevelPresenter
    {
        private int _id;
        private DisposeCommand _disposeCommand;
        private string _levelId;
        private ILevelView _view;
        private ISelectedLevelService _selectedLevelService;
        private ILiveData<bool> _isSelected;
        private ILiveData<bool> _isPassed;

        public LevelPresenter
        (
            int id,
            DisposeCommand disposeCommand,
            GetLevelSelectionQuery getLevelSelectionQuery,
            GetLevelStateQuery getLevelStateQuery,
            GetLevelIdQuery getLevelIdQuery,
            ILevelView view,
            ISelectedLevelService selectedLevelService
        )
        {
            _id = id;
            _disposeCommand = disposeCommand;
            _view = view;
            _selectedLevelService = selectedLevelService;
            _isSelected = getLevelSelectionQuery.Handle(id);
            _isPassed = getLevelStateQuery.Handle(id);
            _levelId = getLevelIdQuery.Handle(id);
        }

        public void Enable()
        {
            _isSelected.AddListener(OnIsSelectedChanged);
            _isPassed.AddListener(OnIsPassedChanged);
            _view.SetLevelName(_levelId); //todo : translate
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

        public void OnViewDispose()
        {
            Dispose();
        }

        private void OnIsPassedChanged(bool isPassed)
        {
            _view.SetPassed(isPassed);
        }

        private void OnIsSelectedChanged(bool isSelected)
        {
            _view.SetSelected(isSelected);
        }

        private void Dispose()
        {
            _disposeCommand.Handle(_id);

            _disposeCommand = null;
            _levelId = null;
            _view = null;
            _selectedLevelService = null;
            _isSelected = null;
            _isPassed = null;
        }
    }
}
