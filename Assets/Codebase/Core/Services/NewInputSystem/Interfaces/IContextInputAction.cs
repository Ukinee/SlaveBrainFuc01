using System;
using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos;

namespace Codebase.Core.Services.NewInputSystem.Interfaces
{
    public interface IContextInputAction : IDisposable
    {
        public string Name { get; }
        
        public void Invoke(IVirtualActionInfo actionInfo);
    }
}
