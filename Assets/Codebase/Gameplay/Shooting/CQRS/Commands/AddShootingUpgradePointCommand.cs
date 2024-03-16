using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.Models;
using Codebase.Gameplay.PlayerData.Services.Interfaces;

namespace Codebase.Gameplay.Shooting.CQRS.Commands
{
    public class AddShootingUpgradePointCommand : EntityUseCaseBase<GameplayPlayerDataModel>
    {
        private readonly IGameplayPlayerDataService _gameplayPlayerDataService;

        public AddShootingUpgradePointCommand(IGameplayPlayerDataService gameplayPlayerDataService, IEntityRepository repository) : base(repository)
        {
            _gameplayPlayerDataService = gameplayPlayerDataService;
        }

        public void Handle()
        {
            GameplayPlayerDataModel playerData = Get(_gameplayPlayerDataService.Id);
            
            playerData.SetUpgradePoints(playerData.UpgradePoints.Value + 1);

            if (playerData.UpgradePoints.Value % GameConstants.UpgradeShootingServiceThreshold != 0)
                return;

            playerData.SetUpgradePoints(0);
            playerData.UpgradeShooting();
        }
    }
}
