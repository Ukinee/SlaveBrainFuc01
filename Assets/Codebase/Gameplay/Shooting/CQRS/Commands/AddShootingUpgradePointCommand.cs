using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.Models;
using Codebase.Gameplay.PlayerData.Services.Interfaces;
using Codebase.Gameplay.Shooting.Services.Interfaces;

namespace Codebase.Gameplay.Shooting.CQRS.Commands
{
    public class AddShootingUpgradePointCommand : EntityUseCaseBase<GameplayPlayerDataModel>
    {
        private readonly IShootingUpgradeService _shootingUpgradeService;
        private readonly IGameplayPlayerDataService _gameplayPlayerDataService;

        public AddShootingUpgradePointCommand(IShootingUpgradeService shootingUpgradeService, IGameplayPlayerDataService gameplayPlayerDataService, IEntityRepository repository) : base(repository)
        {
            _shootingUpgradeService = shootingUpgradeService;
            _gameplayPlayerDataService = gameplayPlayerDataService;
        }

        public void Handle() //todo: refactor
        {
            GameplayPlayerDataModel playerData = Get(_gameplayPlayerDataService.Id);
            
            playerData.SetUpgradePoints(playerData.UpgradePoints.Value + 1);

            if (playerData.UpgradePoints.Value % playerData.MaxUpgradePoints.Value != 0)
                return;

            playerData.SetMaxUpgradePoints(_shootingUpgradeService.GetUpgradeThreshold(playerData.BallsToShoot.Value));
            playerData.SetUpgradePoints(0);
            playerData.UpgradeShooting();
        }
    }
}
