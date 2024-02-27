using System;

namespace Codebase.Core.Services.NewInputSystem.Interfaces
{
    public interface IInputActionWrapper : IDisposable
    {
        public void OnEnable();
        public void OnDisable();
    }
}
