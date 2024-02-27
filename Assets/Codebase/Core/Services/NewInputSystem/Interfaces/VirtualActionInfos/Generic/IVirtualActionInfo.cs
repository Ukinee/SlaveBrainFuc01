namespace Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos.Generic
{
    public interface IVirtualActionInfo<out TPayload> : IVirtualActionInfo
    {
        public TPayload Payload { get; }
    }
}
