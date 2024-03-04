using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;

namespace Codebase.Core.Frameworks.SignalSystem.Common.Signals
{
    public class DisposeSignal : ISignal
    {
        public int Id { get; }

        public DisposeSignal(int id)
        {
            Id = id;
        }
    }
}
