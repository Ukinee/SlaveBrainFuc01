namespace ApplicationCode.Core.Frameworks.SignalSystem.Interfaces
{
    public interface ISignalBus
    {
        public void Handle<T>(T signal) where T : class, ISignal;
    }
}