using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.MainMenu.CQRS.Queries
{
    public class GetShopLevelIdQuery: EntityUseCaseBase<ShopLevelModel>
    {
        public GetShopLevelIdQuery(IEntityRepository repository) : base(repository)
        {
        }

        public string Handle(int id) =>
            Get(id).GamePresetId;
    }
}
