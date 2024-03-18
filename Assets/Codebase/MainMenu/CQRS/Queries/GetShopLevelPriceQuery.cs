using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.MainMenu.CQRS.Queries
{
    public class GetShopLevelPriceQuery: EntityUseCaseBase<ShopLevelModel>
    {
        public GetShopLevelPriceQuery(IEntityRepository repository) : base(repository)
        {
        }

        public int Handle(int id) =>
            Get(id).Price;
    }
}
