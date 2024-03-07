using Codebase.Structures.Models;
using Codebase.Structures.Services.Implementations;
using UnityEngine;

namespace Codebase.Structures.CQRS.Commands
{
    public class CreateStructureCommand
    {
        private readonly StructureService _structureService;
        private readonly StructureCreationService _structureCreationService;

        public CreateStructureCommand(StructureService structureService, StructureCreationService structureCreationService)
        {
            _structureService = structureService;
            _structureCreationService = structureCreationService;
        }
        
        public void Handle(string id, Vector3 position)
        {
            StructureModel structureModel = _structureCreationService.CreateStructure(id, position);
            
            _structureService.Add(structureModel);
        }
    }
}
