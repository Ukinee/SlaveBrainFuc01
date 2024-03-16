using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.Game.CQRS.Queries
{
    public class GetLevelSelectionQuery : EntityUseCaseBase<LevelModel>
    {
        public GetLevelSelectionQuery(IEntityRepository repository) : base(repository)
        {
        }

        public ILiveData<bool> Handle(int id) =>
            Get(id).IsSelected;
    }
}
