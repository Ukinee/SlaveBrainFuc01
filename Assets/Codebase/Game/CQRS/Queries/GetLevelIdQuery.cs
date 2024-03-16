using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.Game.CQRS.Queries
{
    public class GetLevelIdQuery : EntityUseCaseBase<LevelModel>
    {
        public GetLevelIdQuery(IEntityRepository repository) : base(repository)
        {
        }

        public string Handle(int id) =>
            Get(id).LevelId;
    }
}
