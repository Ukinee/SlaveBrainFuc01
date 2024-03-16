using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.Interface.Models;

namespace Codebase.Gameplay.Interface.CQRS.Queries
{
    public class GetWinFormCoinAmountQuery: EntityUseCaseBase<GameplayWinFormModel>
    {
        public GetWinFormCoinAmountQuery(IEntityRepository repository) : base(repository)
        {
        }

        public ILiveData<int> Handle(int id) =>
            Get(id).CoinAmount;
    }
}
