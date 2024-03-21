using Codebase.Gameplay.Shooting.Services.Interfaces;

namespace Codebase.Gameplay.Shooting.Services.Implementations
{
    public class ShootingUpgradeService : IShootingUpgradeService
    {
        public int GetUpgradeThreshold(int currentBalls)
        {
            return (int)(currentBalls * 2.5f) + 1; //todo: formula
        }
    }
}
