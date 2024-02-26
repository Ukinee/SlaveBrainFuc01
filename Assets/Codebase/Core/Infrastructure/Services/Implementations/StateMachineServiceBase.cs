using System;
using System.Collections.Generic;
using Codebase.Core.Infrastructure.Controllers.StateMachines;
using Codebase.Core.Infrastructure.Types.Updates;

namespace Codebase.Core.Infrastructure.Services.Implementations
{
    public abstract class StateMachineServiceBase<TState> : IUpdatable, ILateUpdatable, IFixedUpdatable  where TState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Dictionary<Type, Func<StateMachine, TState>> _stateFactories;

        protected StateMachineServiceBase(IDictionary<Type, Func<StateMachine, TState>> states)
        {
            _stateMachine = new StateMachine();
            _stateFactories = new Dictionary<Type, Func<StateMachine, TState>>(states);
        }
        
        public abstract void Init();
        public abstract void Exit();
        
        public void Update(float deltaTime) =>
            _stateMachine.Update(deltaTime);

        public void LateUpdate(float deltaTime) =>
            _stateMachine.LateUpdate(deltaTime);

        public void FixedUpdate(float deltaTime) =>
            _stateMachine.FixedUpdate(deltaTime);

        protected void SetState<T>() where T : TState
        {
            OnBeforeStateChange<T>();
            _stateMachine.SetCurrentState(_stateFactories[typeof(T)].Invoke(_stateMachine));
            OnAfterStateChange<T>();
        }

        protected virtual void OnBeforeStateChange<T>() where T : TState
        {
        }

        protected virtual void OnAfterStateChange<T>() where T : TState
        {
        }
    }
}
