using System;
using System.Collections.Generic;
using Codebase.Core.Services.Common;
using Cysharp.Threading.Tasks;

namespace Codebase.Core.Infrastructure.StateMachines.Simple
{
    public abstract class StateMachineServiceBase<TState, TPayload> : IUpdatable, ILateUpdatable, IFixedUpdatable, IStateMachineService<TPayload>
        where TState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Dictionary<Type, Func<IStateMachineService<TPayload>, TState>> _stateFactories;

        private bool _isChangingState;
        private Action _delayedStateChange;

        protected StateMachineServiceBase(IDictionary<Type, Func<IStateMachineService<TPayload>, TState>> states)
        {
            _stateMachine = new StateMachine();
            _stateFactories = new Dictionary<Type, Func<IStateMachineService<TPayload>, TState>>(states);
        }

        public abstract void Init();
        public abstract void Exit();

        public void Update(float deltaTime) =>
            _stateMachine.Update(deltaTime);

        public void LateUpdate(float deltaTime) =>
            _stateMachine.LateUpdate(deltaTime);

        public void FixedUpdate(float deltaTime) =>
            _stateMachine.FixedUpdate(deltaTime);

        public async void SetState(TPayload payload)
        {
            if(_isChangingState)
            {
                _delayedStateChange = () => SetState(payload);
                return;
            }
            
            _delayedStateChange = null;
            _isChangingState = true;
            
            await OnBeforeStateChangeAsync(payload);
            _stateMachine.SetCurrentState(_stateFactories[payload.GetType()].Invoke(this));
            await OnAfterStateChangeAsync(payload);
            
            _isChangingState = false;
            _delayedStateChange?.Invoke();
        }

        protected abstract UniTask OnBeforeStateChangeAsync(TPayload payload); 
        protected abstract UniTask OnAfterStateChangeAsync(TPayload payload);
    }
}
