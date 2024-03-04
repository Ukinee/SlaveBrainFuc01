using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;

namespace Assets.Codebase.Core.Frameworks.SignalSystem.General
{
    public class SignalBus : ISignalBus
    {
        private readonly SignalHandler _signalHandler;

        public SignalBus(SignalHandler signalHandler)
        {
            _signalHandler = signalHandler;
        }

        public void Handle<T>(T signal) where T : class, ISignal
        {
            _signalHandler.Handle(signal);
        }
    }
}
