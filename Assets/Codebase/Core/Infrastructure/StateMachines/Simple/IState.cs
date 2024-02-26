namespace Assets.Codebase.Core.Infrastructure.StateMachines.Simple
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}
