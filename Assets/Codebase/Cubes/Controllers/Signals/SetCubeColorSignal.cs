using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using Codebase.Core.Common.Application.Types;

namespace Codebase.Cubes.Controllers.Signals
{
    public class SetCubeColorSignal : ISignal
    {
        public int ID { get; }
        public CubeColor CubeColor { get; }

        public SetCubeColorSignal(int id, CubeColor cubeColor)
        {
            ID = id;
            CubeColor = cubeColor;
        }
    }
}
