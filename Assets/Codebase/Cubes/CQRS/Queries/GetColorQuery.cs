using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Cubes.Models;

namespace Codebase.Cubes.CQRS.Queries
{
    public class GetColorQuery : EntityUseCaseBase<CubeModel>
    {
        public GetColorQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public ILiveData<CubeColor> Handle(int id) =>
            Get(id).Color;
    }
}
