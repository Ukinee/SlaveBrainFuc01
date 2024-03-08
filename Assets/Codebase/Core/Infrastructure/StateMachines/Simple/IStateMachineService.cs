namespace Codebase.Core.Infrastructure.StateMachines.Simple
{
    public interface IStateMachineService<in TPayload>
    {
        public void SetState(TPayload payload);
    }
}
