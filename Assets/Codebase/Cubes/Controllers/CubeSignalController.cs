using System.Collections.Generic;
using Assets.Codebase.Core.Frameworks.SignalSystem.Base;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions;

namespace Codebase.Cubes.Controllers
{
    public class CubeSignalController : SignalControllerBase
    {
        public CubeSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}
