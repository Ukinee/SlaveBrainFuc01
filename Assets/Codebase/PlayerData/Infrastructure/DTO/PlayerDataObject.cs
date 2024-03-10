using System;
using Codebase.Maps.Common;
using UnityEngine.Serialization;

namespace Codebase.PlayerData.Infrastructure.DTO
{
    [Serializable]
    public class PlayerDataObject
    {
        public PlayerDataObject(bool isFirstStart, int coins, int levelsPassed, int[] passedLevels, string[] unlockedStructuresForInfiniteLevels, MapType[] unlockedMaps)
        {
            IsFirstStart = isFirstStart;
            Coins = coins;
            LevelsPassed = levelsPassed;
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
        public int LevelsPassed;
        public int[] PassedLevels;
        public string[] UnlockedStructuresForInfiniteLevels;
        public MapType[] UnlockedMaps;
    }
}
