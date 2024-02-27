using System;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos;
using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos.Generic;

namespace ApplicationCode.Core.Services.NewInputSystem.Common
{
    public abstract class InputContextActionBase<TPayload> : IContextInputAction
    {
        protected InputContextActionBase(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void Invoke(IVirtualActionInfo actionInfo)
        {
            if (actionInfo is not IVirtualActionInfo<TPayload> info)
                throw new ArgumentException($"Action info is not of type {typeof(TPayload).Name}");
            
            if(info.IsDown)
                OnActionStart(info.Payload);
            
            if(info.IsHeld)
                OnActionUpdate(info.Payload);
            
            if(info.IsUp)
                OnActionEnd(info.Payload);
        }

        protected abstract void OnActionStart(TPayload payload);
        protected abstract void OnActionUpdate(TPayload payload);
        protected abstract void OnActionEnd(TPayload payload);

        public abstract void Dispose();
    }
}
