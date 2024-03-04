using System.Collections.Generic;
using System.Linq;
using ApplicationCode.Core.Frameworks.SignalSystem.Interfaces;
using ApplicationCode.Core.Frameworks.SignalSystem.Interfaces.SignalActions;
using ApplicationCode.Core.Frameworks.SignalSystem.Interfaces.SignalActions.Generic;

namespace ApplicationCode.Core.Frameworks.SignalSystem.Base
{
    public abstract class SignalControllerBase : ISignalController
    {
        private readonly List<ISignalAction> _signalActions;

        protected SignalControllerBase(IEnumerable<ISignalAction> signalActions)
        {
            _signalActions = signalActions.ToList();
        }

        public void Handle<T>(T signal) where T : class, ISignal
        {
            foreach (ISignalAction signalAction in _signalActions)
            {
                if (signalAction is ISignalAction<T> typedSignalAction)
                {
                    typedSignalAction.Handle(signal);
                }
            }
        }
    }
}