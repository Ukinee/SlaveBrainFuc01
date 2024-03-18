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
        private LiveData<string[]> _unlockedGamePresets;
        private LiveData<MapType[]> _unlockedMaps;

        public PlayerDataModel
        (
            int id,
            int coins,
            int levelsPassed,
            MapType selectedMap,
            string[] passedLevels,
            string[] unlockedStructures,
            MapType[] unlockedMaps
        ) : base(id)
        {
            SelectedMap = selectedMap;
            _coins = new LiveData<int>(coins);
            _levelsPassed = new LiveData<int>(levelsPassed);
            _passedLevels = new LiveData<string[]>(passedLevels);
            _unlockedGamePresets = new LiveData<string[]>(unlockedStructures);
            _unlockedMaps = new LiveData<MapType[]>(unlockedMaps);
        }

        public MapType SelectedMap { get; private set;  }
        public ILiveData<int> Coins => _coins;
        public ILiveData<int> LevelsPassed => _levelsPassed;
        public ILiveData<IReadOnlyList<string>> PassedLevels => _passedLevels;
        public ILiveData<IReadOnlyList<string>> UnlockedGamePresets => _unlockedGamePresets;
        public ILiveData<IReadOnlyList<MapType>> UnlockedMaps => _unlockedMaps;

        public void SetSelectedMap(MapType selectedMap) =>
            SelectedMap = selectedMap;
        
        public void SetCoins(int coins) =>
            _coins.Value = coins;

        public void AddLevelsAmountPassed() =>
            _levelsPassed.Value++;

        public void AddPassedLevel(string passedLevel)
        {
            if (_passedLevels.Value.Contains(passedLevel) == false)
                _passedLevels.Value = _passedLevels.Value.Concat(new string[] { passedLevel }).ToArray();
        }

        public void AddUnlockedGamePreset(string presetId)
        {
            if (_unlockedGamePresets.Value.Contains(presetId) == false)
                _unlockedGamePresets.Value = _unlockedGamePresets.Value.Concat(new string[] { presetId }).ToArray();
        }

        public void AddUnlockedMap(MapType unlockedMap)
        {
            if (_unlockedMaps.Value.Contains(unlockedMap) == false)
                _unlockedMaps.Value = _unlockedMaps.Value.Concat(new MapType[] { unlockedMap }).ToArray();
        }

        protected override void OnDispose()
        {
            _coins.Dispose();
            _levelsPassed.Dispose();
            _passedLevels.Dispose();
            _unlockedGamePresets.Dispose();
            _unlockedMaps.Dispose();

            _coins = null;
            _levelsPassed = null;
            _passedLevels = null;
            _unlockedGamePresets = null;
            _unlockedMaps = null;
        }
    }
}
