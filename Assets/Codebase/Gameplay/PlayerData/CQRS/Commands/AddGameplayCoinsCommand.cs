using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.Models;
using Codebase.Gameplay.PlayerData.Services.Interfaces;

namespace Codebase.Gameplay.PlayerData.CQRS.Commands
{
    public class AddGameplayCoinsCommand : EntityUseCaseBase<GameplayPlayerDataModel>
    {
        private readonly IGameplayPlayerDataService _idProvider;

        public AddGameplayCoinsCommand(IGameplayPlayerDataService idProvider, IEntityRepository repository) : base(repository)
        {
            _idProvider = idProvider;
        }

        public void Handle(int amount) =>
            Get(_idProvider.Id).AddCoins(amount);
    }
}
