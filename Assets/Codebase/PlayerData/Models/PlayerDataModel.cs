using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Maps.Common;

namespace Codebase.PlayerData.Models
{
    public class PlayerDataModel : BaseEntity
    {
        private LiveData<int> _coins;
        private LiveData<int> _infiniteLevelsPassed;
        private LiveData<int[]> _passedLevels;
        private LiveData<string[]> _unlockedStructures;
        private LiveData<MapType[]> _unlockedMaps;

        public PlayerDataModel
        (
            int id,
            int coins,
            int infiniteLevelsPassed,
            int[] passedLevels,
            string[] unlockedStructures,
            MapType[] unlockedMaps
        ) : base(id)
        {
            _coins = new LiveData<int>(coins);
            _infiniteLevelsPassed = new LiveData<int>(infiniteLevelsPassed);
            _passedLevels = new LiveData<int[]>(passedLevels);
            _unlockedStructures = new LiveData<string[]>(unlockedStructures);
            _unlockedMaps = new LiveData<MapType[]>(unlockedMaps);
        }

        public ILiveData<int> Coins => _coins;
        public ILiveData<int> InfiniteLevelsPassed => _infiniteLevelsPassed;
        public ILiveData<int[]> PassedLevels => _passedLevels;
        public ILiveData<string[]> UnlockedStructures => _unlockedStructures;
        public ILiveData<MapType[]> UnlockedMaps => _unlockedMaps;

        protected override void OnDispose()
        {
            _coins.Dispose();
            _infiniteLevelsPassed.Dispose();
            _passedLevels.Dispose();
            _unlockedStructures.Dispose();
            _unlockedMaps.Dispose();

            _coins = null;
            _infiniteLevelsPassed = null;
            _passedLevels = null;
            _unlockedStructures = null;
            _unlockedMaps = null;
        }
    }
}
