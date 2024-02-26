using Codebase.App.SceneStateMachines.Interfaces.States;

namespace Codebase.App.SceneStateMachines.Interfaces.Services
{
    public interface ISceneStateMachine
    {
        public void ChangeState<T>() where T : ISceneState;
    }
}
