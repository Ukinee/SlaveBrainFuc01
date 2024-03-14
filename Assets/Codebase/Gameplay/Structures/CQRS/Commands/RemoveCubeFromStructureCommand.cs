using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Structures.Models;

namespace Codebase.Structures.CQRS.Commands
{
    public class RemoveCubeFromStructureCommand : EntityUseCaseBase<StructureModel>
    {
        public RemoveCubeFromStructureCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void HandleSafe(int structureId, int cubeId)
        {
            StructureModel structure = Get(structureId);

            structure.TryRemove(cubeId);
        }
    }
}
