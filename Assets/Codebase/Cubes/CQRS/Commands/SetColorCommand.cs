using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Cubes.Models;

namespace Codebase.Cubes.CQRS.Commands
{
    public class SetColorCommand : EntityUseCaseBase<CubeModel>
    {
        public SetColorCommand(IEntityRepository entityRepository) : base(entityRepository)
        {
            
        }

        public void Handle(int id, CubeColor color)
        {
            Get(id).SetColor(color);
        }
    }
}
