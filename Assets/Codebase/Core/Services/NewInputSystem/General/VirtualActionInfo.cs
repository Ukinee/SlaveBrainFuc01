using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos.Generic;

namespace Codebase.Core.Services.NewInputSystem.General
{
    public class VirtualActionInfo<T> : IVirtualActionInfo<T>
    {
        public VirtualActionInfo(string name, T payload, bool isDown, bool isHeld, bool isUp)
        {
            Name = name;
            Payload = payload;
            IsDown = isDown;
            IsHeld = isHeld;
            IsUp = isUp;
        }

        public T Payload { get; }
        public string Name { get; }

        public bool IsDown { get; }
        public bool IsHeld { get; }
        public bool IsUp { get; }
    }
}
