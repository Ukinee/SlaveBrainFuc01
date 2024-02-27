using System.Collections.Generic;
using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos;

namespace Codebase.Core.Services.NewInputSystem.Interfaces
{
    public interface IInputReader
    {
        public IReadOnlyCollection<IVirtualActionInfo> OnUpdate();
    }
}
