using System.Linq;
using Codebase.Game.Data.Common;
using Codebase.Maps.Common;
using Codebase.Maps.Controllers.ServiceCommands;
using Codebase.Maps.Views.Interfaces;
using Codebase.Structures.CQRS.Commands;
using Codebase.Tanks.CQRS;

namespace Codebase.Game.Services.Implementations
{
    public class GameStarter
    {
        private readonly SetTankPositionCommand _setTankPositionCommand;
        private readonly CreateStructureCommand _createStructureCommand;
        private readonly SetObstacleServiceCommand _setObstacleServiceCommand;
        private readonly GamePresets _gamePresets;
        private readonly IMapView _mapView;

        public GameStarter
        (
            SetTankPositionCommand setTankPositionCommand,
            CreateStructureCommand createStructureCommand,
            SetObstacleServiceCommand setObstacleServiceCommand,
            GamePresets gamePresets,
            IMapView mapView
        )
        {
            _setTankPositionCommand = setTankPositionCommand;
            _createStructureCommand = createStructureCommand;
            _setObstacleServiceCommand = setObstacleServiceCommand;
            _gamePresets = gamePresets;
            _mapView = mapView;
        }

        public void Start(string levelId, MapType mapType)
        {
            _setTankPositionCommand.Handle(0.5f);

            _mapView.ShowMap(mapType);
            
            FillMap(levelId);
        }

        private void FillMap(string levelId)
        {
            GamePresetData gamePresetData = _gamePresets.GamePresetDatas.First(x => x.LevelId == levelId);

            foreach (StructurePreset structurePreset in gamePresetData.Structures)
                _createStructureCommand.Handle(structurePreset.StructureId, structurePreset.Position);

            _setObstacleServiceCommand.Set(gamePresetData.ObstacleId);
        }
    }
}
