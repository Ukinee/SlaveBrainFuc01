using System;
using System.Collections.Generic;
using Codebase.Core.Services.Common;
using Cysharp.Threading.Tasks;

namespace Assets.Codebase.Core.Infrastructure.StateMachines.Simple
{
    public abstract class StateMachineServiceBase<TState> : IUpdatable, ILateUpdatable, IFixedUpdatable, IStateMachineService
        where TState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Dictionary<Type, Func<IStateMachineService, TState>> _stateFactories;

        protected StateMachineServiceBase(IDictionary<Type, Func<IStateMachineService, TState>> states)
        {
            _stateMachine = new StateMachine();
            _stateFactories = new Dictionary<Type, Func<IStateMachineService, TState>>(states);
        }

        public abstract void Init();
        public abstract void Exit();

        public void Update(float deltaTime) =>
            _stateMachine.Update(deltaTime);

        public void LateUpdate(float deltaTime) =>
            _stateMachine.LateUpdate(deltaTime);

        public void FixedUpdate(float deltaTime) =>
            _stateMachine.FixedUpdate(deltaTime);

        public async void SetState<T>()
        {
            await OnBeforeStateChangeAsync<T>();
            _stateMachine.SetCurrentState(_stateFactories[typeof(T)].Invoke(this));
            await OnAfterStateChangeAsync<T>();
        }

        protected abstract UniTask OnBeforeStateChangeAsync<T>();
        protected abstract UniTask OnAfterStateChangeAsync<T>();
    }
}
