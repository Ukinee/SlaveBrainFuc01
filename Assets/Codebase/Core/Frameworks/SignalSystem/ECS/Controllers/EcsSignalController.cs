using System.Collections.Generic;
using Assets.Codebase.Core.Frameworks.SignalSystem.Base;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions;

namespace Codebase.Core.Frameworks.SignalSystem.ECS.Controllers
{
    public class EcsSignalController : SignalControllerBase 
    {
        public EcsSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}