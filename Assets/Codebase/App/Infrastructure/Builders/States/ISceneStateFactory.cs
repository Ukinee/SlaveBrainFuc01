using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Infrastructure.StateMachines.Simple;

namespace Codebase.App.Infrastructure.Builders.States
{
    public interface ISceneStateFactory
    {
        public ISceneState CreateSceneState(IStateMachineService<IScenePayload> stateMachineService);
    }
}
