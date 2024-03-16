using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.MainMenu.CQRS.Queries
{
    public class GetLevelStateQuery : EntityUseCaseBase<LevelModel>
    {
        public GetLevelStateQuery(IEntityRepository repository) : base(repository)
        {
        }

        public ILiveData<bool> Handle(int id) =>
            Get(id).IsPassed;
    }
}
