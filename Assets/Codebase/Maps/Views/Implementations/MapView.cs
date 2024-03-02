using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Core.Common.Application.Types;
using Codebase.Maps.Common;
using UnityEngine;

namespace Codebase.Maps.Views.Implementations
{
    public class MapView : MonoBehaviour
    {
        [Serializable]
        class MapData : SerializableDictionary<MapType, GameObject> { }
        
        [SerializeField] private MapData _mapData;
        [SerializeField] private Transform _tankLeftPosition;
        [SerializeField] private Transform _tankRightPosition;
        [SerializeField] private Transform _tankVerticalPosition;

        [SerializeField] private Transform[] _structureSpawnPoints;
        
        private Dictionary<MapType, GameObject> _maps;
        
        public Vector3[] StructureSpawnPoints => _structureSpawnPoints.Select(x => x.position).ToArray();
        public float TankLeftPosition => _tankLeftPosition.position.x;
        public float TankRightPosition => _tankRightPosition.position.x;
        public float TankVerticalPosition => _tankVerticalPosition.position.z;

        private void Awake()
        {
            _maps = _mapData.Dictionary;
        }

        public void ShowMap(MapType mapType)
        {
            foreach ((MapType type, GameObject map) in _maps)
                map.SetActive(mapType == type);
        }
    }
}
