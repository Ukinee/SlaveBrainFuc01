using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.Gameplay.PlayerData.Models
{
    public class GameplayPlayerDataModel : BaseEntity
    {
        private LiveData<int> _coinAmount = new LiveData<int>(0);

        public GameplayPlayerDataModel(int id) : base(id)
        {
        }

        public ILiveData<int> CoinAmount => _coinAmount;
        
        public void AddCoins(int amount) => 
            _coinAmount.Value += amount;

        protected override void OnDispose()
        {
            _coinAmount.Dispose();
            
            _coinAmount = null;
        }
    }
}
