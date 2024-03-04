using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;

namespace Codebase.Cubes.Controllers.Signals
{
    public class ActivateCubeSignal : ISignal
    {
        public int ID { get; }

        public ActivateCubeSignal(int id)
        {
            ID = id;
        }
    }
}
