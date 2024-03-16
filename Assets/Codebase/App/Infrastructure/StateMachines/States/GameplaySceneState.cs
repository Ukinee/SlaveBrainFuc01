using Codebase.Core.Services.Common;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.PauseServices;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Game.Services.Implementations;
using Codebase.Gameplay.PlayerData.Services.Interfaces;
using Codebase.Gameplay.Shooting.Services.Implementations;
using Codebase.Maps.Common;
using UnityEngine;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class GameplaySceneState : ISceneState, IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        private readonly BallMover _ballMover;
        private readonly PauseService _pauseService;
        private readonly InputService _inputService;
        private readonly GameService _gameService;
        private readonly FormCreationService _formCreationService;
        private readonly string _levelId;
        private readonly MapType _mapType;
        private readonly ContextActionService _contextActionService;
        private readonly IGameplayPlayerDataService _gameplayPlayerDataService;
        private readonly IContextInputAction[] _inputActions;

        public GameplaySceneState
        (
            BallMover ballMover,
            PauseService pauseService,
            InputService inputService,
            GameService gameService,
            FormCreationService formCreationService,
            string levelId,
            MapType mapType,
            ContextActionService contextActionService,
            IGameplayPlayerDataService gameplayPlayerDataService,
            IContextInputAction[] inputActions
        )
        {
            _ballMover = ballMover;
            _pauseService = pauseService;
            _inputService = inputService;
            _gameService = gameService;
            _formCreationService = formCreationService;
            _levelId = levelId;
            _mapType = mapType;
            _contextActionService = contextActionService;
            _inputActions = inputActions;
            _gameplayPlayerDataService = gameplayPlayerDataService;
        }

        public void Enter()
        {
            foreach (IContextInputAction inputAction in _inputActions)
                _contextActionService.Register(inputAction);
            
            _formCreationService.Create(new GameplayInterfaceFormType());
            _formCreationService.Create(new GameplayPauseFormType());
            _formCreationService.Create(new GameplayWinFormType());
            
            _inputService.Enable();
            _gameService.Start(_levelId, _mapType);
        }

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                if(_pauseService.IsPaused)
                    _pauseService.ResumeApplication();
                else
                    _pauseService.PauseApplication();
            
            if(Input.GetKeyDown(KeyCode.K))
                _gameService.End();
            
            _inputService.OnUpdate();
            _ballMover.Update(deltaTime);
        }

        public void FixedUpdate(float deltaTime)
        {
        }

        public void LateUpdate(float deltaTime)
        {
        }

        public void Exit()
        {
            foreach (IContextInputAction inputAction in _inputActions)
                _contextActionService.Unregister(inputAction);
            
            _inputService.Disable();
            _gameplayPlayerDataService.Dispose();
        }
    }
}
