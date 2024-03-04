using System;
using System.Collections.Generic;
using ApplicationCode.Core.Frameworks.SignalSystem.Interfaces;

namespace Assets.Codebase.Core.Frameworks.SignalSystem.General
{
    public class SignalHandler
    {
        private List<ISignalController> _controllers = new List<ISignalController>();

        public void Handle<T>(T signal) where T : class, ISignal
        {
            foreach (ISignalController controller in _controllers)
                controller.Handle(signal);
        }

        public void AddController(ISignalController controller)
        {
            if (_controllers.Contains(controller))
                throw new AggregateException();
            
            _controllers.Add(controller);
        }
        
        public void RemoveController(ISignalController controller)
        {
            _controllers.Remove(controller);
        }
    }
}