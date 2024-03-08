using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using UnityEngine;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class MainMenuScene : ISceneState
    {
        private readonly IStateMachineService<IScenePayload> _stateMachineService;

        public MainMenuScene(IStateMachineService<IScenePayload> stateMachineService)
        {
            _stateMachineService = stateMachineService;
        }
        
        public void Enter()
        {
            Debug.Log("Enter MainMenuSceneState");
        }

        public void Exit()
        {
        }
    }
}
