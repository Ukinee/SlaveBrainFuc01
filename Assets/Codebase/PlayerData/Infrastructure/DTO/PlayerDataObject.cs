using System;
using Codebase.Maps.Common;

namespace Codebase.PlayerData.Infrastructure.DTO
{
    [Serializable]
    public class PlayerDataObject
    {
        public PlayerDataObject(bool isFirstStart, int coins, int infiniteLevelsPassed, int[] passedLevels, string[] unlockedStructuresForInfiniteLevels, MapType[] unlockedMaps)
        {
            IsFirstStart = isFirstStart;
            Coins = coins;
            InfiniteLevelsPassed = infiniteLevelsPassed;
            PassedLevels = passedLevels;
            UnlockedStructuresForInfiniteLevels = unlockedStructuresForInfiniteLevels;
            UnlockedMaps = unlockedMaps;
        }

        public static PlayerDataObject Initial => new PlayerDataObject
        (
            true,
            0,
            0,
            Array.Empty<int>(),
            Array.Empty<string>(),
            new[] { MapType.Grass1 }
        );

        public bool IsFirstStart;

        public int Coins;
        public int InfiniteLevelsPassed;
        public int[] PassedLevels;
        public string[] UnlockedStructuresForInfiniteLevels;
        public MapType[] UnlockedMaps;
    }
}
