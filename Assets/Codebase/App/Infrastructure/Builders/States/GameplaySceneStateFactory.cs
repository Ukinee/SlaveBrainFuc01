using System;
using System.Collections.Generic;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using ApplicationCode.Core.Services.Cameras;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Balls.Inputs;
using Codebase.Balls.Services.Implementations;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.Repositories;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.AudioService.Implementation;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.PauseServices;
using Codebase.Core.Services.RaycastHitProviders;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Implementations;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.CQRS;
using Codebase.Forms.Models;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Forms.Services.Interfaces;
using Codebase.Forms.Views.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.Game.Data.Common;
using Codebase.Game.Data.Infrastructure;
using Codebase.Game.Services.Implementations;
using Codebase.Gameplay.Cubes.Controllers.ServiceCommands;
using Codebase.Gameplay.Interface.Services.Implementations;
using Codebase.Gameplay.Interface.Services.Implementations.CreationServices;
using Codebase.Gameplay.PlayerData.CQRS.Commands;
using Codebase.Gameplay.PlayerData.CQRS.Queries;
using Codebase.Gameplay.PlayerData.CreationServices;
using Codebase.Gameplay.PlayerData.Services.Interfaces;
using Codebase.Gameplay.Shooting.CQRS.Commands;
using Codebase.Gameplay.Shooting.CQRS.Queries;
using Codebase.Gameplay.Shooting.Services.Implementations;
using Codebase.Gameplay.Structures.Controllers;
using Codebase.Gameplay.Tanks.Services.Implementations;
using Codebase.Maps.Controllers.ServiceCommands;
using Codebase.Maps.Views.Implementations;
using Codebase.PlayerData.CQRS.Commands;
using Codebase.PlayerData.Services.Implementations;
using Codebase.PlayerData.Services.Interfaces;
using Codebase.Structures.CQRS.Commands;
using Codebase.Tanks.CQRS;
using Codebase.Tanks.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class GameplaySceneStateFactory : ISceneStateFactory
    {
        private readonly EntityRepository _entityRepository;
        private readonly ContextActionService _contextActionService;
        private readonly FilePathProvider _filePathProvider;
        private readonly AssetProvider _assetProvider;
        private readonly IdGenerator _idGenerator;
        private readonly BallViewPool _ballViewPool;
        private readonly CubeViewPool _cubeViewPool;
        private readonly AudioService _audioService;
        private IPlayerIdProvider _playerIdProvider;
        private readonly DataService _dataService;

        public GameplaySceneStateFactory
        (
            EntityRepository entityRepository,
            ContextActionService contextActionService,
            FilePathProvider filePathProvider,
            AssetProvider assetProvider,
            IdGenerator idGenerator,
            BallViewPool ballViewPool,
            CubeViewPool cubeViewPool,
            AudioService audioService,
            IPlayerIdProvider playerIdProvider,
            DataService dataService
        )
        {
            _entityRepository = entityRepository;
            _contextActionService = contextActionService;
            _filePathProvider = filePathProvider;
            _assetProvider = assetProvider;
            _idGenerator = idGenerator;
            _ballViewPool = ballViewPool;
            _cubeViewPool = cubeViewPool;
            _audioService = audioService;
            _playerIdProvider = playerIdProvider;
            _dataService = dataService;
        }

        public ISceneState CreateSceneState
            (IStateMachineService<IScenePayload> stateMachineService, IScenePayload scenePayload)
        {
            if (scenePayload is not GameplayScenePayload gameplayScenePayload)
                throw new Exception("GameplayScenePayload is null");

            PauseService pauseService = new PauseService(_audioService);

            CameraService cameraService = new CameraService();
            cameraService.Set(Camera.main);

            IsCursorOverUiProvider cursorOverUiProvider = new IsCursorOverUiProvider();
            RaycastHitProvider raycastHitProvider = new RaycastHitProvider(cameraService);

            InputServiceFactory inputServiceFactory = new InputServiceFactory
            (
                cursorOverUiProvider,
                _contextActionService
            );

            GameplayPlayerCreationService gameplayPlayerDataCreationService = new GameplayPlayerCreationService
            (
                _entityRepository,
                _idGenerator
            );

            IGameplayPlayerDataService gameplayPlayerDataService = gameplayPlayerDataCreationService.Create();

            AimView aimView = _assetProvider.Instantiate<AimView>(_filePathProvider.General.Data[PathConstants.General.Aim]);

            MapView mapView = Object.FindObjectOfType<MapView>()
                              ?? _assetProvider.Instantiate<MapView>
                                  (_filePathProvider.General.Data[PathConstants.General.Map]);

            SetObstacleServiceCommand setObstacleServiceCommand = new SetObstacleServiceCommand(mapView);

            TankPositionCalculator tankPositionCalculator = new TankPositionCalculator
                (mapView.TankLeftPosition, mapView.TankRightPosition, mapView.TankVerticalPosition);

            TankModel tank = new TankCreationService(tankPositionCalculator, _filePathProvider, _assetProvider).Create();

            GetTankPositionQuery getTankPositionQuery = new GetTankPositionQuery(tank, tankPositionCalculator);
            SetTankPositionCommand setTankPositionCommand = new SetTankPositionCommand(tank);

            TankPositionService tankPositionService = new TankPositionService
            (
                pauseService,
                setTankPositionCommand,
                getTankPositionQuery
            );

            MoveService moveService = new MoveService();
            CollisionService collisionService = new CollisionService();
            BallMover ballMover = new BallMover(moveService);
            BallPoolService ballPoolService = new BallPoolService(_ballViewPool, collisionService, ballMover);

            InputService inputService = inputServiceFactory.Create();

            GetBallsToShootQuery ballsToShootQuery = new GetBallsToShootQuery
            (
                gameplayPlayerDataService,
                _entityRepository
            );

            GetUpgradePointsQuery getUpgradePointsQuery = new GetUpgradePointsQuery
            (
                gameplayPlayerDataService,
                _entityRepository
            );

            ShootingService shootingService = new ShootingService
                (ballsToShootQuery, getTankPositionQuery, ballPoolService, ballMover);

            AimService aimService = new AimService(getTankPositionQuery, aimView);

            CubeViewRepository cubeViewRepository = new CubeViewRepository();
            CubeRepositoryController cubeRepositoryController = new CubeRepositoryController(cubeViewRepository);

            AddGameplayCoinsCommand addGameplayCoinsCommand = new AddGameplayCoinsCommand
            (
                gameplayPlayerDataService,
                _entityRepository
            );

            AddShootingUpgradePointCommand addShootingUpgradePointCommand = new AddShootingUpgradePointCommand
            (
                gameplayPlayerDataService,
                _entityRepository
            );

            GetGameplayPlayerCoinAmountQuery getGameplayPlayerCoinAmountQuery = new GetGameplayPlayerCoinAmountQuery
            (
                gameplayPlayerDataService,
                _entityRepository
            );

            AddPlayerCoinsCommand addPlayerCoinsCommand = new AddPlayerCoinsCommand
            (
                _playerIdProvider,
                _entityRepository
            );

            CubeDeactivatorCollisionHandler cubeDeactivatorCollisionHandler = new CubeDeactivatorCollisionHandler
                (_entityRepository, addGameplayCoinsCommand, addShootingUpgradePointCommand);

            CreateStructureCommandFactory createStructureCommandFactory = new CreateStructureCommandFactory
            (
                _assetProvider,
                _filePathProvider,
                _idGenerator,
                _entityRepository,
                _cubeViewPool,
                cubeRepositoryController,
                cubeViewRepository,
                cubeDeactivatorCollisionHandler
            );

            CreateStructureCommand createStructureCommand = createStructureCommandFactory.Create();

            GamePresets gamePresets = new GamePresetsLoader(_assetProvider, _filePathProvider).Load();

            GameStarter gameStarter = new GameStarter
            (
                setTankPositionCommand,
                createStructureCommand,
                setObstacleServiceCommand,
                gamePresets,
                mapView
            );

            GameEnder gameEnder = new GameEnder(_ballViewPool, _cubeViewPool, shootingService);
            AddPassedLevelCommand addPassedLevelCommand = new AddPassedLevelCommand(_playerIdProvider, _entityRepository);


            #region Interface

            SetFormVisibilityCommand setFormVisibilityCommand = new SetFormVisibilityCommand(_entityRepository);

            string path = _filePathProvider.Forms.Data[PathConstants.Forms.Interface];
            InterfaceView interfaceView = _assetProvider.Instantiate<InterfaceView>(path);
            InterfaceService interfaceService = new InterfaceService(setFormVisibilityCommand);
            
            WinFormService winFormService = new WinFormService(interfaceService, _entityRepository);

            GameService gameService = new GameService
            (
                pauseService,
                gameStarter,
                gameEnder,
                addPassedLevelCommand,
                addPlayerCoinsCommand,
                getGameplayPlayerCoinAmountQuery,
                cubeRepositoryController,
                winFormService,
                stateMachineService,
                _dataService
            );

            GameplayWinFormCreationService winCreationService = new GameplayWinFormCreationService
            (
                _idGenerator,
                _entityRepository,
                _assetProvider,
                interfaceService,
                _audioService,
                _filePathProvider,
                pauseService,
                gameService,
                winFormService
            );

            GameplayPauseFormCreationService pauseCreationService = new GameplayPauseFormCreationService
            (
                _idGenerator,
                _entityRepository,
                _assetProvider,
                interfaceService,
                _audioService,
                _filePathProvider,
                pauseService,
                gameService
            );

            GameplayInterfaceFormCreationService interfaceCreationService = new GameplayInterfaceFormCreationService
            (
                _idGenerator,
                _entityRepository,
                _assetProvider,
                interfaceService,
                _audioService,
                _filePathProvider,
                pauseService,
                gameService,
                getGameplayPlayerCoinAmountQuery,
                ballsToShootQuery,
                getUpgradePointsQuery
            );

            var factories = new Dictionary<Type, Func<Tuple<FormBase, IFormView>>>()
            {
                [typeof(GameplayInterfaceFormType)] = interfaceCreationService.Create,
                [typeof(GameplayPauseFormType)] = pauseCreationService.Create,
                [typeof(GameplayWinFormType)] = winCreationService.Create,
            };

            FormCreationService formCreationService = new FormCreationService(interfaceView, interfaceService, factories);

            #endregion

            int _ = _entityRepository.Count;

            return new GameplaySceneState
            (
                ballMover,
                pauseService,
                inputService,
                gameService,
                formCreationService,
                gameplayScenePayload.LevelId,
                gameplayScenePayload.MapType,
                _contextActionService,
                gameplayPlayerDataService,
                new IContextInputAction[]
                {
                    new ShootInputAction
                    (
                        aimService,
                        shootingService,
                        raycastHitProvider,
                        tankPositionService,
                        cursorOverUiProvider
                    ),
                    new PositionInputActionWrapper(raycastHitProvider, aimService),
                }
            );
        }
    }
}
