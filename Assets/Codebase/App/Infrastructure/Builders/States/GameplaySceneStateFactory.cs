using System.IO;
using ApplicationCode.Core.Services.AssetProviders;
using ApplicationCode.Core.Services.Cameras;
using ApplicationCode.Core.Services.RaycastHitProviders;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.BuildersFactories.States;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Balls.Inputs;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Cubes.Services.Implementations;
using Codebase.Structures.Common;
using Codebase.Structures.Infrastructure.Services.Implementations;
using Codebase.Structures.Services.Implementations;
using Codebase.Tank.Model;
using Codebase.Tanks.CQRS;
using Codebase.Tanks.Services.Implementations;
using Unity.Plastic.Newtonsoft.Json;
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

            CollisionService collisionService = new CollisionService();
            MoveService moveService = new MoveService();

            BallCreationService ballCreationService = new BallCreationService
                (_assetProvider, _filePathProvider, collisionService, moveService);

            BallPool ballPool = new BallPool(ballCreationService.Create);

            TankPositionCalculator tankPositionCalculator = new TankPositionCalculator();
            TankModel tank = new TankCreationService(tankPositionCalculator, _filePathProvider, _assetProvider).Create();

            GetTankPositionQuery tankPositionQuery = new GetTankPositionQuery(tank, tankPositionCalculator);

            ShootingService shootingService = new ShootingService(tankPositionQuery, ballPool, ballMover);
            AimService aimService = new AimService(tankPositionQuery);

            CubeViewFactory cubeViewFactory = new CubeViewFactory(_assetProvider, _filePathProvider);
            CubePool cubePool = new CubePool(cubeViewFactory.Create);
            CubePoolService cubePoolService = new CubePoolService(cubePool);

            StructureReader structureReader = new StructureReader(_assetProvider, _filePathProvider);

            StructureCreationService structureCreationService = new StructureCreationService
            (
                cubePoolService,
                structureReader,
                _assetProvider,
                _filePathProvider
            );

            // CubeDto r = new CubeDto() { Color = CubeColor.Red };
            // CubeDto g = new CubeDto() { Color = CubeColor.Gray };
            // CubeDto b = new CubeDto() { Color = CubeColor.Black };
            // CubeDto t = new CubeDto() { Color = CubeColor.Transparent };
            //
            // StructureDto structureDto = new StructureDto()
            // {
            //     Cubes = new CubeDto[,]
            //     {
            //         {
            //             t, t, r, t, t
            //         },
            //         {
            //             t, g, b, g, t
            //         },
            //         {
            //             b, g, r, g, b
            //         },
            //         {
            //             b, g, r, g, b
            //         },
            //         {
            //             b, b, b, b, b
            //         }
            //     }
            // };
            //
            // string appPath = Application.dataPath;
            // string path = _filePathProvider.Structures.Data[PathConstants.Structures.StructureDirectoryKey];
            // string name = "Tower.json";
            //
            // string json = JsonConvert.SerializeObject(structureDto);
            // File.WriteAllText(Path.Combine(appPath, name), json);

            return new GameplayScene
            (
                ballMover,
                inputService,
                _contextActionService,
                structureCreationService,
                new IContextInputAction[]
                {
                    new PositionInputActionWrapper(raycastHitProvider, aimService),
                    new ShootInputAction(aimService, shootingService),
                }
            );
        }
    }
}
