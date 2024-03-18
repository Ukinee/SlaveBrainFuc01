using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.MainMenu.CQRS.Commands;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.Maps.Common;
using Codebase.PlayerData.CQRS.Commands;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.MainMenu.Services.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IDataService _dataService;
        private readonly PlayerHasEnoughCoinsQuery _playerHasEnoughCoinsQuery;
        private readonly AddUnlockedGamePresetCommand _addUnlockedGamePresetCommand;
        private readonly AddUnlockedMapCommand _addUnlockedMapCommand;
        private readonly RemovePlayerCoinsCommand _removePlayerCoinsCommand;
        
        private readonly MainMenuMapGetPriceQuery _mainMenuMapGetPriceQuery;
        private readonly MainMenuMapGetTypeQuery _mainMenuMapGetTypeQuery;
        private readonly MainMenuMapSetBoughtCommand _mainMenuMapSetBoughtCommand;
        
        private readonly GetShopLevelPriceQuery _getShopLevelPriceQuery;
        private readonly GetShopLevelIdQuery _getShopLevelIdQuery;

        public ShopService
        (
            IEntityRepository entityRepository,
            IPlayerIdProvider playerIdProvider,
            IDataService dataService
        )
        {
            _dataService = dataService;
            _playerHasEnoughCoinsQuery = new PlayerHasEnoughCoinsQuery(playerIdProvider, entityRepository);
            _addUnlockedGamePresetCommand = new AddUnlockedGamePresetCommand(playerIdProvider, entityRepository);
            _addUnlockedMapCommand = new AddUnlockedMapCommand(playerIdProvider, entityRepository);
            _getShopLevelPriceQuery = new GetShopLevelPriceQuery(entityRepository);
            _removePlayerCoinsCommand = new RemovePlayerCoinsCommand(playerIdProvider, entityRepository);
            _mainMenuMapGetTypeQuery = new MainMenuMapGetTypeQuery(entityRepository);
            _mainMenuMapSetBoughtCommand = new MainMenuMapSetBoughtCommand(entityRepository);
            _getShopLevelIdQuery = new GetShopLevelIdQuery(entityRepository);
            _mainMenuMapGetPriceQuery = new MainMenuMapGetPriceQuery(entityRepository);
        }

        public void BuyLevel(int id)
        {
            int price = _getShopLevelPriceQuery.Handle(id);
            
            if (_playerHasEnoughCoinsQuery.Handle(price) == false)
                return;
            
            string presetId = _getShopLevelIdQuery.Handle(id);
            _addUnlockedGamePresetCommand.Handle(presetId);
            _removePlayerCoinsCommand.Handle(price);
            
            _dataService.Save();
        }

        public void BuyMap(int id)
        {
            int price = _mainMenuMapGetPriceQuery.Handle(id);
            
            if (_playerHasEnoughCoinsQuery.Handle(price) == false)
                return;
            
            MapType mapType = _mainMenuMapGetTypeQuery.Handle(id);
            _addUnlockedMapCommand.Handle(mapType);
            _mainMenuMapSetBoughtCommand.Handle(id, true);
            _removePlayerCoinsCommand.Handle(price);
            
            _dataService.Save();
        }
    }
}
