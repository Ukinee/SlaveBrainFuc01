using System;
using System.Collections.Generic;
using ApplicationCode.Core.Common.General.Utils;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Core.Infrastructure.Services.Interfaces;
using Codebase.Core.Services.SceneLoadServices;

namespace Codebase.App.Infrastructure.StateMachines
{
    public class SceneStateMachineService : StateMachineServiceBase<ISceneState>
    {
        private readonly SceneLoadService _sceneLoadService;
        private readonly ICurtain _curtain;

        public SceneStateMachineService
        (
            SceneLoadService sceneLoadService,
            ICurtain curtain,
            IDictionary<Type, Func<IStateMachineService, ISceneState>> stateFactories
        ) : base(stateFactories)
        {
            _sceneLoadService = sceneLoadService;
            _curtain = curtain;
        }

        public override void Init()
        {
            SetState<InitialScene>();
        }

        public override void Exit()
        {
            MaloyAlert.Message("Exiting scene");
        }

        protected override async void OnBeforeStateChange<T>()
        {
            _curtain.Show();
            await _sceneLoadService.LoadSceneAsync(typeof(T).Name);
        }

        protected override void OnAfterStateChange<T>()
        {
            _curtain.Hide();
        }
    }
}
