using System;
using System.Collections.Generic;
using Codebase.App.SceneStateMachines.Implementations.States;
using Codebase.App.SceneStateMachines.Interfaces.Services;
using Codebase.App.SceneStateMachines.Interfaces.States;
using Codebase.Core.Infrastructure.Controllers.StateMachines;
using Codebase.Core.Infrastructure.Services.Implementations;
using Codebase.Core.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Codebase.App.SceneStateMachines.Services
{
    public class SceneStateMachineService : StateMachineServiceBase<ISceneState>, ISceneStateMachine
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ICurtain _curtain;

        public SceneStateMachineService
        (
            ISceneLoadService sceneLoadService,
            ICurtain curtain,
            IDictionary<Type, Func<StateMachine, ISceneState>> states
        )
            : base(states)
        {
            _sceneLoadService = sceneLoadService;
            _curtain = curtain;
        }

        public override void Init() =>
            SetState<InitialSceneState>();

        public void ChangeState<T>() where T : ISceneState =>
            SetState<T>();

        public override void Exit() =>
            Debug.Log("Disposing Game");

        protected override async void OnBeforeStateChange<TState>()
        {
            string stateName = typeof(TState).Name;

            _curtain.Show();
            await _sceneLoadService.LoadSceneAsync(stateName);
        }

        protected override void OnAfterStateChange<TState>()
        {
            _curtain.Hide();
        }
    }
}
