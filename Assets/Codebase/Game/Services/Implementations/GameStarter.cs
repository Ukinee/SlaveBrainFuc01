using Codebase.Game.Data.Common;
using Codebase.Maps.Controllers.ServiceCommands;
using Codebase.Structures.CQRS.Commands;
using Codebase.Tanks.CQRS;
using UnityEngine;

namespace Codebase.Game.Services.Implementations
{
    public class GameStarter
    {
        private readonly SetTankPositionCommand _setTankPositionCommand;
        private readonly CreateStructureCommand _createStructureCommand;
        private readonly SetObstacleServiceCommand _setObstacleServiceCommand;
        private readonly GamePresets _gamePresets;

        public GameStarter
        (
            SetTankPositionCommand setTankPositionCommand,
            CreateStructureCommand createStructureCommand,
            SetObstacleServiceCommand setObstacleServiceCommand,
            GamePresets gamePresets
        )
        {
            _setTankPositionCommand = setTankPositionCommand;
            _createStructureCommand = createStructureCommand;
            _setObstacleServiceCommand = setObstacleServiceCommand;
            _gamePresets = gamePresets;
        }

        public void Start()
        {
            _setTankPositionCommand.Handle(0.5f);

            FillMap();
        }

        private void FillMap()
        {
            int randomIndex = Random.Range(0, _gamePresets.GamePresetDatas.Length);
            GamePresetData gamePresetData = _gamePresets.GamePresetDatas[randomIndex];

            foreach (StructurePreset structurePreset in gamePresetData.Structures)
                _createStructureCommand.Handle(structurePreset.StructureId, structurePreset.Position);
            
            _setObstacleServiceCommand.Set(gamePresetData.ObstacleId);
        }
    }
}
