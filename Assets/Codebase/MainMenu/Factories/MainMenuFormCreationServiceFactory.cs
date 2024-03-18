using System;
using System.Collections.Generic;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.Common;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Models;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Forms.Views.Interfaces;
using Codebase.MainMenu.Common;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Services.Implementations;
using Codebase.MainMenu.Services.Implementations.CreationServices;
using Codebase.MainMenu.Services.Implementations.Repositories;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.MainMenu.Factories
{
    public class MainMenuFormCreationServiceFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IInterfaceService _interfaceService;
        private readonly LevelRepositoryController _levelRepositoryController;
        private readonly MapRepositoryController _mapRepositoryController;
        private readonly IInterfaceView _interfaceView;
        private readonly IAudioService _audioService;
        private readonly ISelectedLevelService _selectedLevelService;
        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;
        private GetPassedLevelsQuery _getPassedLevelsQuery;
        private readonly GetPlayerMapsQuery _getPlayerMapsQuery;
        private readonly GetPlayerSelectedMapQuery _getPlayerSelectedMapQuery;
        private readonly SelectedMapService _selectedMapService;
        private IPlayerIdProvider _playerIdProvider;
        private IStateMachineService<IScenePayload> _stateMachineService;
        private IShopService _shopService;

        public MainMenuFormCreationServiceFactory
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IInterfaceService interfaceService,
            LevelRepositoryController levelRepositoryController,
            MapRepositoryController mapRepositoryController,
            IInterfaceView interfaceView,
            IAudioService audioService,
            ISelectedLevelService selectedLevelService,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            SelectedMapService selectedMapService,
            IPlayerIdProvider playerIdProvider,
            IStateMachineService<IScenePayload> stateMachineService,
            IShopService shopService
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _interfaceService = interfaceService;
            _levelRepositoryController = levelRepositoryController;
            _mapRepositoryController = mapRepositoryController;
            _interfaceView = interfaceView;
            _audioService = audioService;
            _selectedLevelService = selectedLevelService;
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
            _playerIdProvider = playerIdProvider;
            _getPassedLevelsQuery = new GetPassedLevelsQuery(_playerIdProvider, _entityRepository);
            _getPlayerMapsQuery = new GetPlayerMapsQuery(_playerIdProvider, _entityRepository);
            _getPlayerSelectedMapQuery = new GetPlayerSelectedMapQuery(_playerIdProvider, _entityRepository);
            _selectedMapService = selectedMapService;
            _stateMachineService = stateMachineService;
            _shopService = shopService;
        }

        public FormCreationService Create(string[] levelIds, IReadOnlyList<MapShopData> mapData)
        {
            MainMenuFormFactory mainMenuFormFactory = new MainMenuFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _assetProvider,
                _filePathProvider,
                _playerIdProvider
            );

            MainMenuSettingsFormFactory settingsFormFactory = new MainMenuSettingsFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _audioService,
                _assetProvider,
                _filePathProvider
            );

            MainMenuShopFormFactory shopFormFactory = new MainMenuShopFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _assetProvider,
                _filePathProvider
            );

            MainMenuLeaderboardFormFactory leaderboardFormFactory = new MainMenuLeaderboardFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _assetProvider,
                _filePathProvider
            );

            #region LevelSelector

            MainMenuLevelChanger mainMenuLevelChanger = new MainMenuLevelChanger
            (
                new GetLevelIdQuery(_entityRepository),
                _selectedLevelService,
                _selectedMapService,
                _stateMachineService
            );

            LevelSelectorFactory levelSelectorFactory = new LevelSelectorFactory
            (
                _assetProvider,
                _filePathProvider,
                _idGenerator,
                _entityRepository,
                _levelRepositoryController,
                _mapRepositoryController,
                _interfaceService,
                _interfaceView,
                mainMenuLevelChanger
            );

            LevelCreationService levelCreationService = new LevelCreationService
            (
                _idGenerator,
                _assetProvider,
                _filePathProvider,
                _entityRepository,
                _levelRepositoryController,
                _selectedLevelService
            );

            MainMenuMapCreationService mainMenuMapCreationService = new MainMenuMapCreationService
            (
                _idGenerator,
                _assetProvider,
                _entityRepository,
                _mapRepositoryController,
                _selectedMapService,
                _shopService,
                _filePathProvider
            );

            MainMenuLevelSelectorCreationService mainMenuLevelSelectorCreationService =
                new MainMenuLevelSelectorCreationService
                (
                    levelCreationService,
                    levelSelectorFactory,
                    _getPassedLevelsQuery,
                    _getPlayerSelectedMapQuery,
                    _getPlayerMapsQuery,
                    mainMenuMapCreationService,
                    _entityRepository,
                    levelIds,
                    mapData,
                    _selectedMapService
                );

            #endregion

            var factories = new Dictionary<Type, Func<Tuple<FormBase, IFormView>>>()
            {
                [typeof(LevelSelectorFormType)] = mainMenuLevelSelectorCreationService.Create,
                [typeof(MainMenuFormType)] = mainMenuFormFactory.Create,
                [typeof(MainMenuSettingsFormType)] = settingsFormFactory.Create,
                [typeof(MainMenuShopFormType)] = shopFormFactory.Create,
                [typeof(MainMenuLeaderboardFormType)] = leaderboardFormFactory.Create,
            };

            return new FormCreationService(_interfaceView, _interfaceService, factories);
        }
    }
}
