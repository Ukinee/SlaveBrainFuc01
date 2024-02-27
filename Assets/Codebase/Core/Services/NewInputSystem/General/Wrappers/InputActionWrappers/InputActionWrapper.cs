using Codebase.Core.Services.NewInputSystem.Interfaces;
using UnityEngine.InputSystem;

namespace Codebase.Core.Services.NewInputSystem.General.Wrappers.InputActionWrappers
{
    public class InputActionWrapper : IInputActionWrapper
    {
        private ContextActionService _contextActionService;
        private readonly InputAction _inputAction;
        private string _id;

        public InputActionWrapper
        (
            ContextActionService contextActionService,
            InputAction inputAction,
            string id
        )
        {
            _contextActionService = contextActionService;
            _inputAction = inputAction;
            _id = id;
        }

        public void OnEnable()
        {
            _inputAction.started += OnActionStarted;
            _inputAction.performed += OnActionPerformed;
            _inputAction.canceled += OnActionCanceled;
        }

        public void OnActionPerformed(InputAction.CallbackContext obj)
        {
            Handle
            (
                obj,
                false,
                true,
                false
            );
        }

        private void OnActionStarted(InputAction.CallbackContext obj)
        {
            Handle
            (
                obj,
                true,
                false,
                false
            );
        }

        private void OnActionCanceled(InputAction.CallbackContext obj)
        {
            Handle
            (
                obj,
                false,
                false,
                true
            );
        }

        private void Handle
        (
            InputAction.CallbackContext callbackContext,
            bool isDown,
            bool isPressed,
            bool isUp
        )
        {
            _contextActionService.Handle
            (
                new VirtualActionInfo<object>
                (
                    _id,
                    null,
                    isDown,
                    isPressed,
                    isUp
                )
            );
        }

        public void OnDisable()
        {
            _inputAction.started -= OnActionStarted;
            _inputAction.canceled -= OnActionCanceled;
        }

        public void Dispose()
        {
        }
    }
}
