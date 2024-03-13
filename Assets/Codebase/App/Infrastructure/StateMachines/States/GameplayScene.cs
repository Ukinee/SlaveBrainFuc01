using Codebase.Balls.Services.Implementations;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.PauseServices;
using Codebase.Game.Services;
using Codebase.Game.Services.Implementations;
using Codebase.Maps.Common;
using UnityEngine;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class GameplayScene : ISceneState, IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        private readonly BallMover _ballMover;
        private readonly PauseService _pauseService;
        private readonly InputService _inputService;
        private readonly GameService _gameService;
        private readonly string _levelId;
        private readonly MapType _mapType;
        private readonly ContextActionService _contextActionService;
        private readonly IContextInputAction[] _inputActions;

        public GameplayScene
        (
            BallMover ballMover,
            PauseService pauseService,
            InputService inputService,
            GameService gameService,
            string levelId,
            MapType mapType,
            ContextActionService contextActionService,
            IContextInputAction[] inputActions
        )
        {
            _ballMover = ballMover;
            _pauseService = pauseService;
            _inputService = inputService;
            _gameService = gameService;
            _levelId = levelId;
            _mapType = mapType;
            _contextActionService = contextActionService;
            _inputActions = inputActions;
        }

        public void Enter()
        {
            foreach (IContextInputAction inputAction in _inputActions)
                _contextActionService.Register(inputAction);
            
            _inputService.Enable();
            _gameService.Start(_levelId, _mapType);
        }

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                if(_pauseService.IsPaused)
                    _pauseService.ApplicationResume();
                else
                    _pauseService.ApplicationPause();
            
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
        }
    }
}
