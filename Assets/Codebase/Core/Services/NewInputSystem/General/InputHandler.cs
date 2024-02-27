using System;
using System.Collections.Generic;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Core.Services.NewInputSystem.Interfaces.VirtualActionInfos;

namespace Codebase.Core.Services.NewInputSystem.General
{
    [Obsolete("", true)]
    public class InputHandler
    {
        private readonly ContextActionService _contextActionService;
        private IInputReader _activeReader;

        public InputHandler(ContextActionService contextActionService)
        {
            _contextActionService = contextActionService;
        }

        public void SetInputReader(IInputReader inputReader)
        {
            _activeReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        }

        public void OnUpdate()
        {
            if(_activeReader == null)
                return;
            
            IReadOnlyCollection<IVirtualActionInfo> actions = _activeReader.OnUpdate();
            
            //_contextActionService.Handle(actions);
        }
    }
}
