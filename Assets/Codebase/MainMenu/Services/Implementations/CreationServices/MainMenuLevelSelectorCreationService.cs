using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Forms.Models;
using Codebase.Forms.Views.Interfaces;
using Codebase.MainMenu.Common;
using Codebase.MainMenu.CQRS.Commands;
using Codebase.Maps.Common;
using Codebase.PlayerData.CQRS.Queries;

namespace Codebase.MainMenu.Services.Implementations.CreationServices
{
    public class MainMenuLevelSelectorCreationService
    {
        private readonly LevelCreationService _levelCreationService;
        private readonly LevelSelectorFactory _levelSelectorFactory;
        private readonly GetPassedLevelsQuery _getPassedLevelsQuery;
        private readonly GetPlayerSelectedMapQuery _getPlayerSelectedMapQuery;
        private readonly GetPlayerMapsQuery _getPlayerMapsQuery;
        private readonly MainMenuMapCreationService _mainMenuMapCreationService;
        private readonly SelectedMapService _selectedMapService;
        private readonly string[] _levelIds;
        private readonly IReadOnlyList<MapShopData> _mapTypes;
        private readonly AddLevelToLevelSelectionFormCommand _addLevelToLevelSelectionFormCommand;
        private readonly AddMapToLevelSelectionFormCommand _addMapToLevelSelectionFormCommand;

        public MainMenuLevelSelectorCreationService
        (
            LevelCreationService levelCreationService,
            LevelSelectorFactory levelSelectorFactory,
            GetPassedLevelsQuery getPassedLevelsQuery,
            GetPlayerSelectedMapQuery getPlayerSelectedMapQuery,
            GetPlayerMapsQuery getPlayerMapsQuery,
            MainMenuMapCreationService mainMenuMapCreationService,
            IEntityRepository entityRepository,
            string[] levelIds,
            IReadOnlyList<MapShopData> mapTypes,
            SelectedMapService selectedMapService
        )
        {
            _levelCreationService = levelCreationService;
            _levelSelectorFactory = levelSelectorFactory;
            _getPassedLevelsQuery = getPassedLevelsQuery;
            _getPlayerSelectedMapQuery = getPlayerSelectedMapQuery;
            _getPlayerMapsQuery = getPlayerMapsQuery;
            _mainMenuMapCreationService = mainMenuMapCreationService;
            _levelIds = levelIds;
            _mapTypes = mapTypes;
            _selectedMapService = selectedMapService;
            _addLevelToLevelSelectionFormCommand = new AddLevelToLevelSelectionFormCommand(entityRepository);
            _addMapToLevelSelectionFormCommand = new AddMapToLevelSelectionFormCommand(entityRepository);
        }

        public Tuple<FormBase, IFormView> Create()
        {
            Tuple<FormBase, IFormView> tuple = _levelSelectorFactory.Create();

            int selectorFormId = tuple.Item1.Id;
            IReadOnlyList<string> passedLevels = _getPassedLevelsQuery.Handle();

            foreach (string levelStringId in _levelIds)
            {
                int levelId = _levelCreationService.Create(levelStringId, passedLevels.Contains(levelStringId));

                _addLevelToLevelSelectionFormCommand.Handle(selectorFormId, levelId);
            }

            MapType selectedMapType = _getPlayerSelectedMapQuery.Handle();
            IReadOnlyList<MapType> playerMaps = _getPlayerMapsQuery.Handle();

            foreach (MapShopData mapShopData in _mapTypes)
            {
                MapType mapType = mapShopData.Type;
                bool isSelected = mapType == selectedMapType;
                bool isBought = playerMaps.Contains(mapType);

                int mapId = _mainMenuMapCreationService.Create(mapType, mapShopData.Price, isSelected, isBought);

                if (selectedMapType == mapType)
                    _selectedMapService.Select(mapId);

                _addMapToLevelSelectionFormCommand.Handle(selectorFormId, mapId);
            }

            return tuple;
        }
    }
}
