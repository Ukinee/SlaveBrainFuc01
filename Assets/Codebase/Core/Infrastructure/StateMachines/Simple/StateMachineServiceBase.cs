using System;
using System.Collections.Generic;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.Common;
using Cysharp.Threading.Tasks;

namespace Codebase.Core.Infrastructure.StateMachines.Simple
{
    public abstract class StateMachineServiceBase<TState> : IUpdatable, ILateUpdatable, IFixedUpdatable, IStateMachineService
        where TState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Dictionary<Type, Func<IStateMachineService, TState>> _stateFactories;

        private bool _isChangingState;
        private Action _delayedStateChange;

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
            if(_isChangingState)
            {
                _delayedStateChange = SetState<T>;
                return;
            }
            
            _delayedStateChange = null;
            _isChangingState = true;
            
            await OnBeforeStateChangeAsync<T>();
            _stateMachine.SetCurrentState(_stateFactories[typeof(T)].Invoke(this));
            await OnAfterStateChangeAsync<T>();
            
            _isChangingState = false;
            _delayedStateChange?.Invoke();
        }

        protected abstract UniTask OnBeforeStateChangeAsync<T>();
        protected abstract UniTask OnAfterStateChangeAsync<T>();
    }
}
