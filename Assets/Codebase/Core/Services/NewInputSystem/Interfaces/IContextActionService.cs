namespace Codebase.Core.Services.NewInputSystem.Interfaces
{
    public interface IContextActionService
    {
        public void Register(IContextInputAction handler);
        public void Unregister(IContextInputAction handler);
    }
}
