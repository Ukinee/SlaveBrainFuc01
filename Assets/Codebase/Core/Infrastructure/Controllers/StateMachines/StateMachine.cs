using Codebase.Core.Infrastructure.Types.Updates;

namespace Codebase.Core.Infrastructure.Controllers.StateMachines
{
    public sealed class StateMachine : IFixedUpdatable, IUpdatable, ILateUpdatable
    {
        private IState _currentState;
        
        public void SetCurrentState(IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
        
        public void FixedUpdate(float deltaTime)
        {
            if(_currentState is IFixedUpdatable fixedUpdatable)
                fixedUpdatable.FixedUpdate(deltaTime);
        }

        public void Update(float deltaTime)
        {
            if(_currentState is IUpdatable updatable)
                updatable.Update(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            if(_currentState is ILateUpdatable lateUpdatable)
                lateUpdatable.LateUpdate(deltaTime);
        }
    }
}
