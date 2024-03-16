using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.Gameplay.PlayerData.Models
{
    public class GameplayPlayerDataModel : BaseEntity
    {
        private LiveData<int> _coinAmount = new LiveData<int>(0);

        private LiveData<int> _ballsToShoot;
        private LiveData<int> _upgradePoints = new LiveData<int>(0);

        public GameplayPlayerDataModel(int id, int amountToShoot) : base(id)
        {
            _ballsToShoot = new LiveData<int>(amountToShoot);
        }

        public ILiveData<int> CoinAmount => _coinAmount;

        public ILiveData<int> BallsToShoot => _ballsToShoot;
        public ILiveData<int> UpgradePoints => _upgradePoints;

        public void AddCoins(int amount) =>
            _coinAmount.Value += amount;

        public void SetUpgradePoints(int value) =>
            _upgradePoints.Value = value;

        public void UpgradeShooting() =>
            _ballsToShoot.Value++;

        protected override void OnDispose()
        {
            _coinAmount.Dispose();

            _coinAmount = null;
        }
    }
}
