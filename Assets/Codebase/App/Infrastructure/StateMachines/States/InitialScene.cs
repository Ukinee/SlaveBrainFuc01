using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Infrastructure.StateMachines.Simple;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class InitialScene : ISceneState
    {
        private readonly IStateMachineService<IScenePayload> _stateMachineService;

        public InitialScene(IStateMachineService<IScenePayload> stateMachineService)
        {
            _stateMachineService = stateMachineService;
        }
        
        public void Enter()
        {
            _stateMachineService.SetState(new MainMenuScenePayload());
        }

        public void Exit()
        {
        }
    }
}
