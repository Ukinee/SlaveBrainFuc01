using System.Collections.Generic;
using Codebase.Core.Services.NewInputSystem.Interfaces;

namespace Codebase.Core.Services.NewInputSystem.General
{
    public class InputService
    {
        private readonly List<IInputActionMapWrapper> _actionMapWrappers;

        private readonly IsCursorOverUiProvider _isCursorOverUiProvider;
        private readonly GeneralInput _generalInput;

        public InputService
        (
            IsCursorOverUiProvider isCursorOverUiProvider,
            GeneralInput generalInput,
            IEnumerable<IInputActionMapWrapper> actionMapWrappers
        )
        {
            _isCursorOverUiProvider = isCursorOverUiProvider;
            _generalInput = generalInput;
            _actionMapWrappers = new List<IInputActionMapWrapper>(actionMapWrappers);
        }

        public void Enable()
        {
            foreach (IInputActionMapWrapper actionMapWrapper in _actionMapWrappers)
                actionMapWrapper.Enable();

            _generalInput.Enable();
        }

        public void OnUpdate()
        {
            _isCursorOverUiProvider.OnUpdate();
            
            foreach (IInputActionMapWrapper actionMapWrapper in _actionMapWrappers)
            {
                actionMapWrapper.OnUpdate();
            }
        }

        public void Disable()
        {
            foreach (IInputActionMapWrapper actionMapWrapper in _actionMapWrappers)
                actionMapWrapper.Disable();

            _generalInput.Disable();
        }
    }
}
