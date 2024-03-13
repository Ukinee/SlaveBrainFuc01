using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.PauseServices;
using Codebase.Maps.Common;
using Codebase.Maps.Views.Interfaces;

namespace Codebase.Game.Services.Implementations
{
    public class GameService
    {
        private readonly GameStarter _gameStarter;
        private readonly GameEnder _gameEnder;
        private readonly IStateMachineService<IScenePayload> _stateMachineService;
        private readonly PauseService _pauseService;

        
        public GameService
        (
            PauseService pauseService,
            GameStarter gameStarter,
            GameEnder gameEnder,
            IStateMachineService<IScenePayload> stateMachineService
        )
        {
            _pauseService = pauseService;
            _gameStarter = gameStarter;
            _gameEnder = gameEnder;
            _stateMachineService = stateMachineService;
        }

        public void Start(string levelId, MapType mapType)
        {
            _gameStarter.Start(levelId, mapType);
        }

        public void End()
        {
            if (_pauseService.IsPaused)
            {
                MaloyAlert.Warning("Unusual behavior: game ended while pause. Resuming game...");
                _pauseService.ApplicationResume();
            }
            
            _gameEnder.End();
            _stateMachineService.SetState(new MainMenuScenePayload());
        }
    }
}
