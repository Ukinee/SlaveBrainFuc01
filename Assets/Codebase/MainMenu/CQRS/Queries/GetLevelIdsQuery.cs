using System.Collections.Generic;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.MainMenu.CQRS.Queries
{
    public class GetLevelIdsQuery : EntityUseCaseBase<MainMenuLevelSelectorFormModel>
    {
        public GetLevelIdsQuery(IEntityRepository repository) : base(repository)
        {
        }

        public ILiveData<IReadOnlyList<int>> Handle(int id) =>
            Get(id).Levels;
    }
}
