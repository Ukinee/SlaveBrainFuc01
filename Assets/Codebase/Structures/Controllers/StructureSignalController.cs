using System.Collections.Generic;
using Assets.Codebase.Core.Frameworks.SignalSystem.Base;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions;

namespace Codebase.Structures.Controllers
{
    public class StructureSignalController : SignalControllerBase
    {
        public StructureSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}
