using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class InitialScene : ISceneState
    {
        private readonly IStateMachineService _stateMachineService;

        public InitialScene(IStateMachineService stateMachineService)
        {
            _stateMachineService = stateMachineService;
        }
        
        public void Enter()
        {
            _stateMachineService.SetState<GameplayScene>();
        }

        public void Exit()
        {
        }
    }
}
