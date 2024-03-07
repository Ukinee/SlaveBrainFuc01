using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using ApplicationCode.Core.Services.Cameras;
using ApplicationCode.Core.Services.RaycastHitProviders;
using Assets.Codebase.Core.Frameworks.EnitySystem.Repositories;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Balls.Inputs;
using Codebase.Balls.Services.Implementations;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Services.AudioService.Implementation;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.PauseServices;
using Codebase.Cubes.Services.Implementations;
using Codebase.Maps.Views.Implementations;
using Codebase.Structures.Controllers;
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

        public ISceneState CreateSceneState(IStateMachineService stateMachineService)
        {
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

            TankPositionCalculator tankPositionCalculator = new TankPositionCalculator
                (mapView.TankLeftPosition, mapView.TankRightPosition, mapView.TankVerticalPosition);

            TankModel tank = new TankCreationService(tankPositionCalculator, _filePathProvider, _assetProvider).Create();

            GetTankPositionQuery getTankPositionQuery = new GetTankPositionQuery(tank, tankPositionCalculator);
            SetTankPositionCommand setTankPositionCommand = new SetTankPositionCommand(tank);

            TankPositionService tankPositionService = new TankPositionService
            (
                pauseService,
                tankPositionCalculator,
                setTankPositionCommand,
                getTankPositionQuery
            );

            MoveService moveService = new MoveService();
            CollisionService collisionService = new CollisionService();
            BallMover ballMover = new BallMover(moveService);
            BallPoolService ballPoolService = new BallPoolService(_ballViewPool, collisionService, ballMover, tankPositionService);

            InputService inputService = inputServiceFactory.Create();

            
            ShootingService shootingService = new ShootingService(getTankPositionQuery, ballPoolService, ballMover);
            AimService aimService = new AimService(getTankPositionQuery, aimView);


            CreateStructureCommandFactory createStructureCommandFactory = new CreateStructureCommandFactory
            (
                _assetProvider,
                _filePathProvider,
                _idGenerator,
                _entityRepository,
                _cubeViewPool
            );

            setTankPositionCommand.Handle(0.5f);
            

            return new GameplayScene
            (
                ballMover,
                pauseService,
                inputService,
                _contextActionService,
                createStructureCommandFactory.Create(),
                new IContextInputAction[]
                {
                    new ShootInputAction(aimService, shootingService, raycastHitProvider),
                    new PositionInputActionWrapper(raycastHitProvider, aimService),
                }
            );
        }
    }
}
