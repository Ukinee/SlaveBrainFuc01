using Codebase.App.SceneStateMachines.Interfaces.States;
using UnityEngine;

namespace Codebase.App.SceneStateMachines.Implementations.States
{
    public class MainMenuSceneState : ISceneState
    {
        public void Enter()
        {
            Debug.Log("Enter MainMenuSceneState");
        }

        public void Exit()
        {
        }
    }
}
