using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.StateMachines.States;

namespace Codebase.App.Infrastructure.Builders.States
{
    public interface ISceneStateFactory
    {
        public ISceneState CreateSceneState(IStateMachineService stateMachineService);
    }
}
