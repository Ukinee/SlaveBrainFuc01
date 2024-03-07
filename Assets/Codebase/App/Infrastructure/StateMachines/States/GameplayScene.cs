using Codebase.Balls.Services.Implementations;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Structures.CQRS.Commands;
using UnityEngine;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class GameplayScene : ISceneState, IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        private readonly BallMover _ballMover;
        private readonly InputService _inputService;
        private readonly ContextActionService _contextActionService;
        private readonly CreateStructureCommand _createStructureCommand;
        private readonly IContextInputAction[] _inputActions;

        public GameplayScene
        (
            BallMover ballMover,
            InputService inputService,
            ContextActionService contextActionService,
            CreateStructureCommand createStructureCommand,
            IContextInputAction[] inputActions
        )
        {
            _ballMover = ballMover;
            _inputService = inputService;
            _contextActionService = contextActionService;
            _createStructureCommand = createStructureCommand;
            _inputActions = inputActions;
        }

        public void Enter()
        {
            foreach (IContextInputAction inputAction in _inputActions)
                _contextActionService.Register(inputAction);
            
            _inputService.Enable();
            
            _createStructureCommand.Handle("Tower", new Vector3(-3, 0, 7));
            _createStructureCommand.Handle("Tower", new Vector3(3, 0, 7));
        }

        public void Update(float deltaTime)
        {
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
