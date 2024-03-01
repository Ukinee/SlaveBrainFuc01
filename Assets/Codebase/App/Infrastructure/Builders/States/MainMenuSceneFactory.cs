using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.StateMachines.States;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class MainMenuSceneFactory : ISceneStateFactory
    {
        public ISceneState CreateSceneState(IStateMachineService stateMachineService)
        {
            return new MainMenuScene(stateMachineService);
        }
    }
}
