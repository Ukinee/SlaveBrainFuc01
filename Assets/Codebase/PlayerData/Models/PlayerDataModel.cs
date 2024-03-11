using System.Collections.Generic;
using System.Linq;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Maps.Common;

namespace Codebase.PlayerData.Models
{
    public class PlayerDataModel : BaseEntity
    {
        private LiveData<int> _coins;
        private LiveData<int> _levelsPassed;
        private LiveData<string[]> _passedLevels;
        private LiveData<string[]> _unlockedStructures;
        private LiveData<MapType[]> _unlockedMaps;

        public PlayerDataModel
        (
            int id,
            int coins,
            int levelsPassed,
            string[] passedLevels,
            string[] unlockedStructures,
            MapType[] unlockedMaps
        ) : base(id)
        {
            _coins = new LiveData<int>(coins);
            _levelsPassed = new LiveData<int>(levelsPassed);
            _passedLevels = new LiveData<string[]>(passedLevels);
            _unlockedStructures = new LiveData<string[]>(unlockedStructures);
            _unlockedMaps = new LiveData<MapType[]>(unlockedMaps);
        }

        public ILiveData<int> Coins => _coins;
        public ILiveData<int> LevelsPassed => _levelsPassed;
        public ILiveData<IReadOnlyList<string>> PassedLevels => _passedLevels;
        public ILiveData<IReadOnlyList<string>> UnlockedStructures => _unlockedStructures;
        public ILiveData<IReadOnlyList<MapType>> UnlockedMaps => _unlockedMaps;

        public void SetCoins(int coins) =>
            _coins.Value = coins;

        public void AddLevelsPassed() =>
            _levelsPassed.Value++;

        public void AddPassedLevel(string passedLevel) =>
            _passedLevels.Value = _passedLevels.Value.Concat(new string[] { passedLevel }).ToArray();

        public void AddUnlockedStructure(string unlockedStructure) =>
            _unlockedStructures.Value = _unlockedStructures.Value.Concat(new string[] { unlockedStructure }).ToArray();

        public void AddUnlockedMap(MapType unlockedMap) =>
            _unlockedMaps.Value = _unlockedMaps.Value.Concat(new MapType[] { unlockedMap }).ToArray();

        protected override void OnDispose()
        {
            _coins.Dispose();
            _levelsPassed.Dispose();
            _passedLevels.Dispose();
            _unlockedStructures.Dispose();
            _unlockedMaps.Dispose();

            _coins = null;
            _levelsPassed = null;
            _passedLevels = null;
            _unlockedStructures = null;
            _unlockedMaps = null;
        }
    }
}
