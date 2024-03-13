using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Core.Common.Application.Types;
using Codebase.Maps.Common;
using Codebase.Maps.Views.Interfaces;
using UnityEngine;

namespace Codebase.Maps.Views.Implementations
{
    public class MapView : MonoBehaviour, IMapView
    {
        [Serializable]
        class MapData : SerializableDictionary<MapType, GameObject> { }        
        
        [Serializable]
        class ObstacleData : SerializableDictionary<string, GameObject> { }

        [SerializeField] private MapData _mapData;
        [SerializeField] private ObstacleData _obstacleData;
        
        [SerializeField] private Transform _tankLeftPosition;
        [SerializeField] private Transform _tankRightPosition;
        [SerializeField] private Transform _tankVerticalPosition;

        [SerializeField] private Transform[] _structureSpawnPoints;
        
        private Dictionary<MapType, GameObject> _maps;
        private Dictionary<string, GameObject> _obstacles;

        public Vector3[] StructureSpawnPoints => _structureSpawnPoints.Select(x => x.position).ToArray();
        public float TankLeftPosition => _tankLeftPosition.position.x;
        public float TankRightPosition => _tankRightPosition.position.x;
        public float TankVerticalPosition => _tankVerticalPosition.position.z;

        private void Awake()
        {
            _maps = _mapData.Dictionary;
            _obstacles = _obstacleData.Dictionary;
        }

        public void ShowMap(MapType mapType)
        {
            foreach ((MapType type, GameObject map) in _maps)
                map.SetActive(mapType == type);
        }

        public void SetObstacle(string obstacleId)
        {
            foreach ((string id, GameObject obstacle) in _obstacles)
                obstacle.SetActive(obstacleId == id);
        }

        public void HideObstacle()
        {
            foreach ((string _, GameObject obstacle) in _obstacles)
                obstacle.SetActive(false);
        }
    }
}
