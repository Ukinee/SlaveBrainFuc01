namespace Codebase.Core.Infrastructure.Controllers.StateMachines
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}
