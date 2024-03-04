namespace ApplicationCode.Core.Frameworks.SignalSystem.Interfaces

{
    public interface ISignalController
    {
        public void Handle<T>(T signal) where T : class, ISignal;
    }
}