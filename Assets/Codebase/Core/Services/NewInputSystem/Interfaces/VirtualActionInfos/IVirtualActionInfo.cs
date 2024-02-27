namespace Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos
{
    public interface IVirtualActionInfo
    {
        public string Name { get; }

        public bool IsDown { get; }
        public bool IsHeld { get; }
        public bool IsUp { get; }
    }
}
