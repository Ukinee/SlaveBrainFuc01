using Assets.Codebase.Core.Frameworks.SignalSystem.General;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Structures.Controllers.Signals;
using UnityEngine;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class GameplayScene : ISceneState, IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        private readonly SignalHandler _signalHandler;
        private readonly BallMover _ballMover;
        private readonly InputService _inputService;
        private readonly ContextActionService _contextActionService;
        private readonly ISignalController[] _signalControllers;
        private readonly IContextInputAction[] _inputActions;

        public GameplayScene
        (
            SignalHandler signalHandler,
            BallMover ballMover,
            InputService inputService,
            ContextActionService contextActionService,
            ISignalController[] signalControllers,
            IContextInputAction[] inputActions
        )
        {
            _signalHandler = signalHandler;
            _ballMover = ballMover;
            _inputService = inputService;
            _contextActionService = contextActionService;
            _signalControllers = signalControllers;
            _inputActions = inputActions;
        }

        public void Enter()
        {
            foreach (ISignalController signalController in _signalControllers)
                _signalHandler.AddController(signalController);
            
            foreach (IContextInputAction inputAction in _inputActions)
                _contextActionService.Register(inputAction);
            
            _inputService.Enable();
            
            _signalHandler.Handle(new CreateStructureSignal("Tower", Vector3.forward * 7));
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
            
            
            foreach (ISignalController signalController in _signalControllers)
                _signalHandler.RemoveController(signalController);
            
            _inputService.Disable();
        }
    }
}
