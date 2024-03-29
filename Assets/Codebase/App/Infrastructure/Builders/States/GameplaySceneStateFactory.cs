﻿using ApplicationCode.Core.Services.AssetProviders;
using ApplicationCode.Core.Services.Cameras;
using ApplicationCode.Core.Services.RaycastHitProviders;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Balls.Inputs;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Services.AudioService.Implementation;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Cubes.Services.Implementations;
using Codebase.Maps.Views.Implementations;
using Codebase.Structures.Infrastructure.Services.Implementations;
using Codebase.Structures.Services.Implementations;
using Codebase.Tanks.CQRS;
using Codebase.Tanks.Model;
using Codebase.Tanks.Services.Implementations;
using UnityEngine;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class GameplaySceneStateFactory : ISceneStateFactory
    {
        private readonly ContextActionService _contextActionService;
        private readonly FilePathProvider _filePathProvider;
        private readonly AssetProvider _assetProvider;
        private readonly CubeRepository _cubeRepository;
        private readonly BallViewPool _ballViewPool;
        private readonly CubePoolService _cubePoolService;
        private readonly AudioService _audioService;

        public GameplaySceneStateFactory
        (
            ContextActionService contextActionService,
            FilePathProvider filePathProvider,
            AssetProvider assetProvider,
            CubeRepository cubeRepository,
            BallViewPool ballViewPool,
            CubePoolService cubePoolService,
            AudioService audioService
        )
        {
            _contextActionService = contextActionService;
            _filePathProvider = filePathProvider;
            _assetProvider = assetProvider;
            _cubeRepository = cubeRepository;
            _ballViewPool = ballViewPool;
            _cubePoolService = cubePoolService;
            _audioService = audioService;
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

            MoveService moveService = new MoveService();
            CollisionService collisionService = new CollisionService();
            BallMover ballMover = new BallMover(moveService);
            BallPoolService ballPoolService = new BallPoolService(_ballViewPool, collisionService, ballMover); 

            InputService inputService = inputServiceFactory.Create();

            MapView mapView = Object.FindObjectOfType<MapView>() ??
                              _assetProvider.Instantiate<MapView>(_filePathProvider.General.Data[PathConstants.General.Map]);

            TankPositionCalculator tankPositionCalculator = new TankPositionCalculator
                (mapView.TankLeftPosition, mapView.TankRightPosition, mapView.TankVerticalPosition);

            TankModel tank = new TankCreationService(tankPositionCalculator, _filePathProvider, _assetProvider).Create();

            GetTankPositionQuery tankPositionQuery = new GetTankPositionQuery(tank, tankPositionCalculator);
            SetTankPositionCommand setTankPositionCommand = new SetTankPositionCommand(tank);

            ShootingService shootingService = new ShootingService(tankPositionQuery, ballPoolService, ballMover);
            AimService aimService = new AimService(tankPositionQuery);

            StructureReader structureReader = new StructureReader(_assetProvider, _filePathProvider);

            StructureViewFactory structureViewFactory = new StructureViewFactory(_assetProvider, _filePathProvider);
            FragmentationService fragmentationService = new FragmentationService(structureViewFactory, _cubeRepository);

            StructureCreationService structureCreationService = new StructureCreationService
            (
                _cubePoolService,
                structureReader,
                fragmentationService,
                structureViewFactory
            );
            
            setTankPositionCommand.Handle(0.5f);

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
