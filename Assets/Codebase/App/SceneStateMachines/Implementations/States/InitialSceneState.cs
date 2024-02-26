using Codebase.App.SceneStateMachines.Interfaces.Services;
using Codebase.App.SceneStateMachines.Interfaces.States;

namespace Codebase.App.SceneStateMachines.Implementations.States
{
    public class InitialSceneState : ISceneState
    {
        private readonly ISceneStateMachine _sceneStateMachine;

        public InitialSceneState(ISceneStateMachine sceneStateMachine)
        {
            _sceneStateMachine = sceneStateMachine;
        }
        
        public void Enter()
        {
            _sceneStateMachine.ChangeState<MainMenuSceneState>();
        }

        public void Exit()
        {
        }
    }
}
