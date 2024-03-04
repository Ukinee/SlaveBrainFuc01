using System.Collections.Generic;
using ApplicationCode.Core.Frameworks.SignalSystem.Base;
using ApplicationCode.Core.Frameworks.SignalSystem.Interfaces.SignalActions;

namespace Codebase.Core.Frameworks.SignalSystem.ECS.Controllers
{
    public class EcsSignalController : SignalControllerBase 
    {
        public EcsSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}