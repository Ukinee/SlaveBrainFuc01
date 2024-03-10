using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Commands
{
    public class RemovePlayerCoinsCommand : EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public RemovePlayerCoinsCommand(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public void Handle(int coinsToRemove)
        {
            if(coinsToRemove <= 0)
                throw new System.InvalidOperationException("Invalid coins amount");
            
            PlayerDataModel model = Get(_playerIdProvider.Id);
            
            if(model.Coins.Value < coinsToRemove)
                throw new System.Exception("Not enough coins!");
            
            model.SetCoins(model.Coins.Value - coinsToRemove);
        }
    }
}
