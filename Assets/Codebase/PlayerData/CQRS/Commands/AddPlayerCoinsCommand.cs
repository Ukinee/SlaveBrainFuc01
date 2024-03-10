using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Commands
{
    public class AddPlayerCoinsCommand : EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public AddPlayerCoinsCommand(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public void Handle(int coinsToAdd)
        {
            if(coinsToAdd <= 0)
                throw new InvalidOperationException("Invalid coins amount");
            
            PlayerDataModel model = Get(_playerIdProvider.Id);
            
            model.SetCoins(model.Coins.Value + coinsToAdd);
        }
    }
}
