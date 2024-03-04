using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions.Generic;
using Codebase.Structures.Controllers.Signals;
using Codebase.Structures.Services.Implementations;

namespace Codebase.Structures.Controllers.Actions
{
    public class CreateStructureSignalAction : ISignalAction<CreateStructureSignal>
    {
        private readonly StructureService _structureService;
        private readonly StructureCreationService _structureCreationService;

        public CreateStructureSignalAction(StructureService structureService, StructureCreationService structureCreationService)
        {
            _structureService = structureService;
            _structureCreationService = structureCreationService;
        }

        public void Handle(CreateStructureSignal signal)
        {
            _structureService.Add(_structureCreationService.CreateStructure(signal.StructureName, signal.Position));
        }
    }
}
