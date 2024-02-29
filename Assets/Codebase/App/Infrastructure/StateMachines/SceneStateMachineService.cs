using System;
using System.Collections.Generic;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Infrastructure.Curtain;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.SceneLoadServices;
using Cysharp.Threading.Tasks;

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
            MaloyAlert.Message("Disposing game");
        }

        protected override async UniTask OnBeforeStateChangeAsync<T>()
        {
            _curtain.Show();
            await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstants.CurtainAnimationTime));
            await _sceneLoadService.LoadSceneAsync(typeof(T).Name);
        }

        protected override async UniTask OnAfterStateChangeAsync<T>()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstants.CurtainAnimationTime));
            _curtain.Hide();
        }
    }
}
