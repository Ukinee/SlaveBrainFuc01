using System;

namespace Codebase.Core.Services.NewInputSystem.Interfaces
{
    public interface IInputActionMapWrapper : IDisposable
    {
        public void Enable();
        public void OnUpdate();
        public void Disable();
    }
}
