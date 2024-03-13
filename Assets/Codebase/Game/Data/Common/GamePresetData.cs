using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.Game.Data.Common
{
    [Serializable]
    public class GamePresetData
    {
        public GamePresetData(StructurePreset[] structures, string obstacleId, string levelId)
        {
            Structures = structures;
            ObstacleId = obstacleId;
            LevelId = levelId;
        }

        [JsonProperty] public string LevelId { get; private set; }
        [JsonProperty] public StructurePreset[] Structures { get; private set; }
        [JsonProperty] public string ObstacleId { get; private set; }
    }

    [Serializable]
    public class StructurePreset
    {
        public StructurePreset(string structureId, Vector3 position)
        {
            StructureId = structureId;
            Position = position;
        }

        public string StructureId {get; private set; }
        public Vector3 Position {get; private set; }
    }
}
