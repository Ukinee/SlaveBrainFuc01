namespace ApplicationCode.Core.Frameworks.SignalSystem.Interfaces.SignalActions.Generic
{
    public interface ISignalAction<in T> : ISignalAction where T : class, ISignal
    {
        void Handle(T signal);
    }
}