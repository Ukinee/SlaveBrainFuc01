using System.Collections.Generic;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using UnityEngine.InputSystem;

namespace Codebase.Core.Services.NewInputSystem.General.Wrappers.ActionMapWrappers.Base
{
    public class InputActionMapWrapper : IInputActionMapWrapper
    {
        private InputActionMap _actionMap;
        private IReadOnlyCollection<IInputActionWrapper> _actionWrappers;

        public InputActionMapWrapper(InputActionMap actionMap, IReadOnlyCollection<IInputActionWrapper> actionWrappers)
        {
            _actionMap = actionMap;
            _actionWrappers = actionWrappers;
        }

        public void Enable()
        {
            foreach (IInputActionWrapper wrapper in _actionWrappers)
                wrapper.OnEnable();
            
            _actionMap.Enable();
        }

        public void Disable()
        {
            foreach (IInputActionWrapper wrapper in _actionWrappers)
                wrapper.OnDisable();
            
            _actionMap.Disable();
        }

        public void OnUpdate()
        {
        }

        public void Dispose()
        {
            _actionMap = default;

            foreach (IInputActionWrapper wrapper in _actionWrappers)
                wrapper.Dispose();

            _actionWrappers = null;
        }
    }
}
