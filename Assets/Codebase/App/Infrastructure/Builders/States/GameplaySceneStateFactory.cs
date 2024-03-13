using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using ApplicationCode.Core.Services.Cameras;
using ApplicationCode.Core.Services.RaycastHitProviders;
using Assets.Codebase.Core.Frameworks.EnitySystem.Repositories;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Balls.Inputs;
using Codebase.Balls.Services.Implementations;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.AudioService.Implementation;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.PauseServices;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Implementations;
using Codebase.Game.Data.Common;
using Codebase.Game.Data.Infrastructure;
using Codebase.Game.Services;
using Codebase.Game.Services.Implementations;
using Codebase.Maps.Controllers.ServiceCommands;
using Codebase.Maps.Views.Implementations;
using Codebase.Structures.Controllers;
using Codebase.Structures.CQRS.Commands;
using Codebase.Tanks.CQRS;
using Codebase.Tanks.Model;
using Codebase.Tanks.Services.Implementations;
using UnityEngine;

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

        public GameplaySceneStateFactory
        (
            EntityRepository entityRepository,
            ContextActionService contextActionService,
            FilePathProvider filePathProvider,
            AssetProvider assetProvider,
            IdGenerator idGenerator,
            BallViewPool ballViewPool,
            CubeViewPool cubeViewPool,
            AudioService audioService
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
        }

        public ISceneState CreateSceneState
            (IStateMachineService<IScenePayload> stateMachineService, IScenePayload scenePayload)
        {
            if (scenePayload is not GameplayScenePayload gameplayScenePayload)
                throw new System.Exception("GameplayScenePayload is null");

            #region InitFiles

            // StructurePreset leftTowerStructurePreset = new StructurePreset("Tower", new Vector3(-3, 0, 5));
            // StructurePreset rightTowerStructurePreset = new StructurePreset("Tower", new Vector3(3, 0, 5));
            // StructurePreset middleTowerStructurePreset = new StructurePreset("Tower", new Vector3(0, 0, 5));
            //
            // GamePresetData twoTowersPreset = new GamePresetData
            // (
            //     new[]
            //     {
            //         leftTowerStructurePreset,
            //         rightTowerStructurePreset,
            //     },
            //     "Cube"
            // );
            //
            // GamePresetData threeTowersPreset = new GamePresetData
            // (
            //     new[]
            //     {
            //         leftTowerStructurePreset,
            //         rightTowerStructurePreset,
            //         middleTowerStructurePreset
            //     },
            //     "Cube"
            // );
            //
            // GamePresetData[] gamePresetDatas = new GamePresetData[]
            // {
            //     twoTowersPreset,
            //     threeTowersPreset
            // };
            //
            // GamePresets testGamePresets = new GamePresets(gamePresetDatas);
            //
            // string jsonPath = Application.dataPath + "/Art/Resources/" + _filePathProvider.Game.Data[PathConstants.Game.GamePresets] + ".json";
            // string json = JsonUtility.ToJson(testGamePresets);
            // File.WriteAllText(jsonPath, json);

            #endregion

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

            ShootingService shootingService = new ShootingService(getTankPositionQuery, ballPoolService, ballMover);
            AimService aimService = new AimService(getTankPositionQuery, aimView);

            CubeViewRepository cubeViewRepository = new CubeViewRepository();
            CubeRepositoryController cubeRepositoryController = new CubeRepositoryController(cubeViewRepository);

            CreateStructureCommandFactory createStructureCommandFactory = new CreateStructureCommandFactory
            (
                _assetProvider,
                _filePathProvider,
                _idGenerator,
                _entityRepository,
                _cubeViewPool,
                cubeRepositoryController,
                cubeViewRepository
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

            GameService gameService = new GameService
            (
                pauseService,
                gameStarter,
                gameEnder,
                cubeRepositoryController,
                stateMachineService
            );

            return new GameplayScene
            (
                ballMover,
                pauseService,
                inputService,
                gameService,
                gameplayScenePayload.LevelId,
                gameplayScenePayload.MapType,
                _contextActionService,
                new IContextInputAction[]
                {
                    new ShootInputAction(aimService, shootingService, raycastHitProvider, tankPositionService),
                    new PositionInputActionWrapper(raycastHitProvider, aimService),
                }
            );
        }
    }
}
