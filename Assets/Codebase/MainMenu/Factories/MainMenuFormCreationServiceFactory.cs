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
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Services.Implementations;
using Codebase.MainMenu.Services.Implementations.CreationServices;
using Codebase.MainMenu.Services.Implementations.Repositories;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.Maps.Common;
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
        private readonly GetPlayerSelectedMapQuery _getPlayerSelectedMapQuery;
        private readonly SelectedMapService _selectedMapService;
        private IPlayerIdProvider _playerIdProvider;
        private IStateMachineService<IScenePayload> _stateMachineService;

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
            GetPassedLevelsQuery getPassedLevelsQuery,
            GetPlayerSelectedMapQuery getPlayerSelectedMapQuery,
            SelectedMapService selectedMapService,
            IPlayerIdProvider playerIdProvider,
            IStateMachineService<IScenePayload> stateMachineService
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
            _getPassedLevelsQuery = getPassedLevelsQuery;
            _getPlayerSelectedMapQuery = getPlayerSelectedMapQuery;
            _selectedMapService = selectedMapService;
            _playerIdProvider = playerIdProvider;
            _stateMachineService = stateMachineService;
        }

        public FormCreationService Create(string[] levelIds, MapType[] mapTypes)
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
                _filePathProvider
            );

            MainMenuLevelSelectorCreationService mainMenuLevelSelectorCreationService =
                new MainMenuLevelSelectorCreationService
                (
                    levelCreationService,
                    levelSelectorFactory,
                    _getPassedLevelsQuery,
                    _getPlayerSelectedMapQuery,
                    mainMenuMapCreationService,
                    _entityRepository,
                    levelIds,
                    mapTypes,
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
