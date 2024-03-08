using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.Game.Data.Common
{
    [Serializable]
    public class GamePresetData
    {
        public GamePresetData(StructurePreset[] structures, string obstacleId)
        {
            Structures = structures;
            ObstacleId = obstacleId;
        }

        [JsonProperty] public StructurePreset[] Structures;
        [JsonProperty] public string ObstacleId;
    }

    [Serializable]
    public class StructurePreset
    {
        public StructurePreset(string id, Vector3 position)
        {
            Id = id;
            Position = position;
        }

        public string Id;
        public Vector3 Position;
    }
}
