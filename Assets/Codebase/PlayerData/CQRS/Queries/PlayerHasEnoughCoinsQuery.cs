using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Queries
{
    public class PlayerHasEnoughCoinsQuery : EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public PlayerHasEnoughCoinsQuery(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public bool Handle(int coinAmount)
        {
            PlayerDataModel model = Get(_playerIdProvider.Id);
            
            return model.Coins.Value >= coinAmount;
        }
    }
}
