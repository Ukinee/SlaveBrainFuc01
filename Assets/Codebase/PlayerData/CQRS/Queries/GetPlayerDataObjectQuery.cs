using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.PlayerData.Infrastructure.DTO;
using Codebase.PlayerData.Models;

namespace Codebase.PlayerData.CQRS.Queries
{
    public class GetPlayerDataObjectQuery : EntityUseCaseBase<PlayerDataModel>
    {
        public GetPlayerDataObjectQuery(IEntityRepository repository) : base(repository)
        {
        }

        public PlayerDataObject Handle(int id)
        {
            PlayerDataModel model = Get(id);

            return new PlayerDataObject
            (
                false,
                model.Coins.Value,
                model.InfiniteLevelsPassed.Value,
                model.PassedLevels.Value,
                model.UnlockedStructures.Value,
                model.UnlockedMaps.Value
            );
        }
    }
}
