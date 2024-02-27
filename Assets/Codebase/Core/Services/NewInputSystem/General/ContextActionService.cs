using System.Collections.Generic;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos;

namespace Codebase.Core.Services.NewInputSystem.General
{
    public class ContextActionService : IContextActionService
    {
        private readonly Dictionary<string, IContextInputAction> _contextActions = new Dictionary<string, IContextInputAction>();

        public void Handle(IVirtualActionInfo virtualActionInfo)
        {
            if(virtualActionInfo.IsDown == false && virtualActionInfo.IsHeld == false && virtualActionInfo.IsUp == false)
                return;

            if (_contextActions.TryGetValue(virtualActionInfo.Name, out IContextInputAction contextAction) == false)
                return;
            
            contextAction.Invoke(virtualActionInfo);
        }

        public void Register(IContextInputAction handler)
        {
            _contextActions.Add(handler.Name, handler);
        }

        public void Unregister(IContextInputAction handler)
        {
            _contextActions.Remove(handler.Name);
        }
    }
}
