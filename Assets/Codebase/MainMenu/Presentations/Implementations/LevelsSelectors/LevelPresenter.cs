using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;

namespace Codebase.MainMenu.Presentations.Implementations.LevelsSelectors
{
    public class LevelPresenter : ILevelPresenter
    {
        private int _id;
        private DisposeCommand _disposeCommand;
        private IShopService _shopService;
        private string _levelId;
        private ILevelView _view;
        private ISelectedLevelService _selectedLevelService;
        private ILiveData<bool> _isSelected;
        private ILiveData<bool> _isPassed;
        private ILiveData<bool> _isUnlocked;

        private int _price;
        
        public LevelPresenter
        (
            int id,
            DisposeCommand disposeCommand,
            GetLevelUnlockStatusQuery getLevelUnlockStatusQuery,
            GetLevelPriceQuery getLevelPriceQuery,
            GetLevelSelectionQuery getLevelSelectionQuery,
            GetLevelPassStatusQuery getLevelPassStatusQuery,
            GetLevelIdQuery getLevelIdQuery,
            IShopService shopService,
            ILevelView view,
            ISelectedLevelService selectedLevelService
        )
        {
            _id = id;
            _disposeCommand = disposeCommand;
            _shopService = shopService;
            _view = view;
            _selectedLevelService = selectedLevelService;
            _isSelected = getLevelSelectionQuery.Handle(id);
            _isPassed = getLevelPassStatusQuery.Handle(id);
            _levelId = getLevelIdQuery.Handle(id);
            _isUnlocked = getLevelUnlockStatusQuery.Handle(id);
            _price = getLevelPriceQuery.Handle(id);
        }

        public void Enable()
        {
            _isSelected.AddListener(OnIsSelectedChanged);
            _isPassed.AddListener(OnIsPassedChanged);
            _isUnlocked.AddListener(OnIsUnlockedChanged);
            _view.SetLevelName(_levelId); //todo : translate
            _view.SetPrice(_price);
        }

        public void Disable()
        {
            _isSelected.RemoveListener(OnIsSelectedChanged);
            _isPassed.RemoveListener(OnIsPassedChanged);
            _isUnlocked.RemoveListener(OnIsUnlockedChanged);
        }

        public void OnSelectButtonClick() =>
            _selectedLevelService.Select(_id);

        public void OnBuyButtonClick() =>
            _shopService.BuyLevel(_id);

        public void OnViewDispose() =>
            Dispose();

        private void OnIsUnlockedChanged(bool isUnlocked) =>
            _view.SetUnlocked(isUnlocked);

        private void OnIsPassedChanged(bool isPassed) =>
            _view.SetPassed(isPassed);

        private void OnIsSelectedChanged(bool isSelected) =>
            _view.SetSelected(isSelected);

        private void Dispose()
        {
            _disposeCommand.Handle(_id);

            _shopService = null;
            _isUnlocked = null;
            _disposeCommand = null;
            _levelId = null;
            _view = null;
            _selectedLevelService = null;
            _isSelected = null;
            _isPassed = null;
        }
    }
}
