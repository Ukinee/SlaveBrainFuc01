using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using Codebase.PlayerData.CQRS.Queries;

namespace Codebase.MainMenu.Services.Implementations.CreationServices
{
    public class ShopLevelPresenter : IShopLevelPresenter
    {
        private readonly int _id;
        private readonly IShopService _shopService;
        private readonly DisposeCommand _disposeCommand;
        private readonly IShopLevelView _shopLevelView;

        private readonly string _gamePresetId;
        private readonly int _price;
        
        private ILiveData<int> _playerCoinLiveData;
        private bool _isBought;

        public ShopLevelPresenter
        (
            int id,
            GetShopLevelPriceQuery getShopLevelPriceQuery,
            GetShopLevelIdQuery getShopLevelIdQuery,
            GetCoinAmountQuery getCoinAmountQuery,
            IShopService shopService,
            DisposeCommand disposeCommand,
            IShopLevelView shopLevelView
        )
        {
            _id = id;
            _shopService = shopService;
            _disposeCommand = disposeCommand;
            _shopLevelView = shopLevelView;
            
            _playerCoinLiveData = getCoinAmountQuery.Handle();
            _gamePresetId = getShopLevelIdQuery.Handle(_id);
            _price = getShopLevelPriceQuery.Handle(_id);
        }

        public void Enable()
        {
            _playerCoinLiveData.AddListener(OnPlayerCoinChanged);
            _shopLevelView.SetPrice(_price);
            _shopLevelView.SetId(_gamePresetId);
        }

        public void Disable()
        {
            _playerCoinLiveData.RemoveListener(OnPlayerCoinChanged);
        }

        public void OnClick()
        {
            if (_isBought)
                return;
            
            _isBought = true;
            _shopService.BuyLevel(_id);
        }

        private void OnPlayerCoinChanged(int amount)
        {
            _shopLevelView.SetAvailability(amount >= _price);
        }

        public void OnViewDisposed() =>
            Dispose();

        private void Dispose()
        {
            _disposeCommand.Handle(_id);
            _playerCoinLiveData.RemoveListener(OnPlayerCoinChanged);
            _playerCoinLiveData = null;
        }
    }
}
