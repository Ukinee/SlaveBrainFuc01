using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.Common;
using Codebase.Forms.CQRS;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Forms.Services.Interfaces;
using Codebase.Forms.Views.Implementations;
using Codebase.MainMenu.Common;
using Codebase.MainMenu.CQRS.Commands;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.DataServices;
using Codebase.MainMenu.Factories;
using Codebase.MainMenu.Services.Implementations;
using Codebase.MainMenu.Services.Implementations.Repositories;
using Codebase.Maps.Common;
using Codebase.PlayerData.CQRS.Commands;
using Codebase.PlayerData.Services.Implementations;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class MainMenuSceneFactory : ISceneStateFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IAudioService _audioService;
        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;
        private IPlayerIdProvider _playerIdProvider;
        private readonly DataService _dataService;

        public MainMenuSceneFactory
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IAudioService audioService,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IPlayerIdProvider playerIdProvider,
            DataService dataService
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
            _playerIdProvider = playerIdProvider;
            _dataService = dataService;
        }

        public ISceneState CreateSceneState
            (IStateMachineService<IScenePayload> stateMachineService, IScenePayload scenePayload)
        {
            #region Configs

            string[] availableLevelIds =
            {
                "Test Two Towers",
                "Test Three Towers",
            };

            MapType[] mapTypes =
            {
                MapType.Grass1,
                MapType.Desert1,
                MapType.Desert2,
                MapType.Jungle1,
                MapType.Snow1,
            };

            #endregion

            #region PlayerData

            SetPlayerSelectedMapCommand playerSelectedMapCommand = new SetPlayerSelectedMapCommand
                (_playerIdProvider, _entityRepository);

            #endregion

            #region Shop

            ShopDataLoader shopDataLoader = new ShopDataLoader
                (_entityRepository, _assetProvider, _filePathProvider, _playerIdProvider);

            ShopData shopData = shopDataLoader.Load(availableLevelIds, mapTypes);

            ShopService shopService = new ShopService(_entityRepository, _playerIdProvider, _dataService);

            #endregion

            SetFormVisibilityCommand setFormVisibilityCommand = new SetFormVisibilityCommand(_entityRepository);
            InterfaceService interfaceService = new InterfaceService(setFormVisibilityCommand);

            string path = _filePathProvider.Forms.Data[PathConstants.Forms.Interface];
            InterfaceView interfaceView = _assetProvider.Instantiate<InterfaceView>(path);

            LevelRepositoryController levelRepositoryController = new LevelRepositoryController();
            SetLevelSelectionCommand setLevelSelectionCommand = new SetLevelSelectionCommand(_entityRepository);
            SelectedLevelService selectedLevelService = new SelectedLevelService(setLevelSelectionCommand);

            MainMenuMapGetTypeQuery mainMenuMapGetTypeQuery = new MainMenuMapGetTypeQuery(_entityRepository);
            SetMapSelectionCommand setMapSelectionCommand = new SetMapSelectionCommand(_entityRepository);

            SelectedMapService selectedMapService = new SelectedMapService
                (setMapSelectionCommand, mainMenuMapGetTypeQuery, playerSelectedMapCommand);

            MapRepositoryController mapRepositoryController = new MapRepositoryController();

            MainMenuFormCreationServiceFactory mainMenuFormCreationServiceFactory = new MainMenuFormCreationServiceFactory
            (
                _idGenerator,
                _entityRepository,
                interfaceService,
                levelRepositoryController,
                mapRepositoryController,
                interfaceView,
                _audioService,
                selectedLevelService,
                _assetProvider,
                _filePathProvider,
                selectedMapService,
                _playerIdProvider,
                stateMachineService,
                shopService
            );

            FormCreationService formCreationService = mainMenuFormCreationServiceFactory
                .Create(shopData.Structures, shopData.Maps);

            MainMenuFactory mainMenuFactory = new MainMenuFactory(formCreationService);

            return new MainMenuScene(mainMenuFactory);
        }
    }
}
