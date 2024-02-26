namespace Codebase.Core.Infrastructure.Types.Updates
{
    public interface ILateUpdatable
    {
        public void LateUpdate(float deltaTime);
    }
}
