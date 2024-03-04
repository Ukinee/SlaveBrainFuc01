using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions.Generic;
using Codebase.Cubes.Controllers.Signals;
using Codebase.Structures.Services.Interfaces;

namespace Codebase.Cubes.Controllers.Actions
{
    public class ActivateCubeSignalAction : ISignalAction<ActivateCubeSignal>
    {
        private readonly IStructureService _structureService;

        public ActivateCubeSignalAction(IStructureService structureService)
        {
            _structureService = structureService;
        }
        
        public void Handle(ActivateCubeSignal signal)
        {
            _structureService.RemoveCube(signal.ID);
        }
    }
}
