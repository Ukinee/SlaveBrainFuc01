using System;
using System.Collections.Generic;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Infrastructure.Curtain;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.SceneLoadServices;
using Cysharp.Threading.Tasks;

namespace Codebase.App.Infrastructure.StateMachines
{
    public class SceneStateMachineService : StateMachineServiceBase<ISceneState, IScenePayload>
    {
        private readonly SceneLoadService _sceneLoadService;
        private readonly ICurtain _curtain;

        public SceneStateMachineService
        (
            SceneLoadService sceneLoadService,
            ICurtain curtain,
            IDictionary<Type, Func<IStateMachineService<IScenePayload>, ISceneState>> stateFactories
        ) : base(stateFactories)
        {
            _sceneLoadService = sceneLoadService;
            _curtain = curtain;
        }

        public override void Init()
        {
            SetState(new InitialScenePayload());
        }

        public override void Exit()
        {
            MaloyAlert.Message("Disposing game");
        }

        protected override async UniTask OnBeforeStateChangeAsync(IScenePayload payload)
        {
            _curtain.Show();
            await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstants.CurtainAnimationTime));
            await _sceneLoadService.LoadSceneAsync(payload.SceneName);
        }

        protected override async UniTask OnAfterStateChangeAsync(IScenePayload payload)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstants.CurtainAnimationTime));
            _curtain.Hide();
        }
    }
}
