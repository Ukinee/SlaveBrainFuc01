using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.Gameplay.PlayerData.Models
{
    public class GameplayPlayerDataModel : BaseEntity
    {
        private LiveData<int> _coinAmount = new LiveData<int>(0);

        private LiveData<int> _upgradePoints = new LiveData<int>(0);
        private LiveData<int> _maxUpgradePoints;
        private LiveData<int> _ballsToShoot;

        public GameplayPlayerDataModel(int id, int amountToShoot, int maxUpgradePoints) : base(id)
        {
            _ballsToShoot = new LiveData<int>(amountToShoot);
            _maxUpgradePoints= new LiveData<int>(maxUpgradePoints);
        }

        public ILiveData<int> CoinAmount => _coinAmount;

        public ILiveData<int> BallsToShoot => _ballsToShoot;
        public ILiveData<int> UpgradePoints => _upgradePoints;
        public ILiveData<int> MaxUpgradePoints => _maxUpgradePoints;

        public void AddCoins(int amount) =>
            _coinAmount.Value += amount;

        public void SetUpgradePoints(int value) =>
            _upgradePoints.Value = value;

        public void SetMaxUpgradePoints(int value) =>
            _maxUpgradePoints.Value = value;

        public void UpgradeShooting() =>
            _ballsToShoot.Value++;

        protected override void OnDispose()
        {
            _coinAmount.Dispose();

            _coinAmount = null;
        }
    }
}
