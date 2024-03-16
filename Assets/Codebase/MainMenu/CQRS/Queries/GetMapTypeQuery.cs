using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.CQRS.Queries
{
    public class GetMapTypeQuery : EntityUseCaseBase<MainMenuMapModel>
    {
        public GetMapTypeQuery(IEntityRepository repository) : base(repository)
        {
        }

        public MapType Handle(int id) =>
            Get(id).MapType;
    }
}
