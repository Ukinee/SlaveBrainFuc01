using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.Game.Data.Common
{
    [Serializable]
    public class GamePresetData
    {
        public GamePresetData(StructurePreset[] structures, string obstacleId, string gamePresetId)
        {
            Structures = structures;
            ObstacleId = obstacleId;
            GamePresetId = gamePresetId;
        }

        [JsonProperty] public string GamePresetId { get; private set; }
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
