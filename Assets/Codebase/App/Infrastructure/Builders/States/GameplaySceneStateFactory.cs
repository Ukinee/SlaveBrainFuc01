using ApplicationCode.Core.Services.AssetProviders;
using ApplicationCode.Core.Services.Cameras;
using ApplicationCode.Core.Services.RaycastHitProviders;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.BuildersFactories.States;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Balls.Inputs;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Tank.Model;
using Codebase.Tanks.CQRS;
using Codebase.Tanks.Services.Implementations;
using UnityEngine;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class GameplaySceneStateFactory : ISceneStateFactory
    {
        private readonly ContextActionService _contextActionService;
        private readonly FilePathProvider _filePathProvider;
        private readonly AssetProvider _assetProvider;

        public GameplaySceneStateFactory
        (
            ContextActionService contextActionService,
            FilePathProvider filePathProvider,
            AssetProvider assetProvider
        )
        {
            _contextActionService = contextActionService;
            _filePathProvider = filePathProvider;
            _assetProvider = assetProvider;
        }

        public ISceneState CreateSceneState(IStateMachineService stateMachineService)
        {
            CameraService cameraService = new CameraService();
            cameraService.Set(Camera.main);

            IsCursorOverUiProvider cursorOverUiProvider = new IsCursorOverUiProvider();
            RaycastHitProvider raycastHitProvider = new RaycastHitProvider(cameraService);

            InputServiceFactory inputServiceFactory = new InputServiceFactory
            (
                cursorOverUiProvider,
                _contextActionService
            );

            InputService inputService = inputServiceFactory.Create();
            BallMover ballMover = new BallMover();
            BallPool ballPool = new BallPool(_assetProvider, _filePathProvider);

            TankPositionCalculator tankPositionCalculator = new TankPositionCalculator();
            TankModel tank = new TankCreationService(tankPositionCalculator, _filePathProvider, _assetProvider).Create();

            GetTankPositionQuery tankPositionQuery = new GetTankPositionQuery(tank, tankPositionCalculator);

            ShootingService shootingService = new ShootingService(tankPositionQuery, ballPool, ballMover);
            AimService aimService = new AimService();

            return new GameplayScene
            (
                ballMover,
                inputService,
                _contextActionService,
                new IContextInputAction[]
                {
                    new PositionInputActionWrapper(raycastHitProvider, aimService),
                    new ShootInputAction(aimService, shootingService),
                }
            );
        }
    }
}
