using System;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Maps.Common;
using Unity.Plastic.Newtonsoft.Json;

namespace Codebase.PlayerData.Infrastructure.DTO
{
    [Serializable]
    public class PlayerDataObject
    {
        public PlayerDataObject
        (
            bool isFirstStart,
            int coins,
            int levelsPassed,
            MapType selectedMap,
            string[] passedLevels,
            string[] unlockedStructures,
            MapType[] unlockedMaps
        )
        {
            IsFirstStart = isFirstStart;
            Coins = coins;
            LevelsPassed = levelsPassed;
            SelectedMap = selectedMap;
            PassedLevels = passedLevels;
            UnlockedStructures = unlockedStructures;
            UnlockedMaps = unlockedMaps;
        }

        public static PlayerDataObject Initial => new PlayerDataObject
        (
            true,
            0,
            0,
            MapType.Grass1,
            Array.Empty<string>(),
            new[] { StructuresConstants.TwoTowersId },
            new[] { MapType.Grass1 }
        );

        [JsonProperty] public bool IsFirstStart { get; private set; }

        [JsonProperty] public int Coins { get; private set; }
        [JsonProperty] public int LevelsPassed { get; private set; }
        [JsonProperty] public string[] PassedLevels { get; private set; }
        [JsonProperty] public string[] UnlockedStructures { get; private set; }
        [JsonProperty] public MapType[] UnlockedMaps { get; private set; }
        [JsonProperty] public MapType SelectedMap { get; private set; }
    }
}
