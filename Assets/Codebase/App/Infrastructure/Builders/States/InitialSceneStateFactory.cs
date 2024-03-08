using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Infrastructure.StateMachines.Simple;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class InitialSceneStateFactory : ISceneStateFactory
    {
        public ISceneState CreateSceneState(IStateMachineService<IScenePayload> stateMachineService)
        {
            return new InitialScene(stateMachineService);
        }
    }
}
