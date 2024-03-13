using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.PauseServices;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Maps.Common;
using Codebase.Maps.Views.Interfaces;

namespace Codebase.Game.Services.Implementations
{
    public class GameService
    {
        private readonly GameStarter _gameStarter;
        private readonly GameEnder _gameEnder;
        private readonly CubeRepositoryController _cubeRepositoryController;
        private readonly IStateMachineService<IScenePayload> _stateMachineService;
        private readonly PauseService _pauseService;

        
        public GameService
        (
            PauseService pauseService,
            GameStarter gameStarter,
            GameEnder gameEnder,
            CubeRepositoryController cubeRepositoryController,
            IStateMachineService<IScenePayload> stateMachineService
        )
        {
            _pauseService = pauseService;
            _gameStarter = gameStarter;
            _gameEnder = gameEnder;
            _cubeRepositoryController = cubeRepositoryController;
            _stateMachineService = stateMachineService;
        }

        public void Start(string levelId, MapType mapType)
        {
            _cubeRepositoryController.OnCubeAmountChanged += OnCubeAmountChanged;
            _gameStarter.Start(levelId, mapType);
        }

        public void End()
        {
            _cubeRepositoryController.OnCubeAmountChanged -= OnCubeAmountChanged;
            
            if (_pauseService.IsPaused)
            {
                MaloyAlert.Warning("Unusual behavior: game ended while pause.");
                _pauseService.ApplicationResume();
            }
            
            if(_cubeRepositoryController.Count == 0)
            {
                //todo: handling prize logic
            }
            
            _gameEnder.End();
            _stateMachineService.SetState(new MainMenuScenePayload());
        }

        private void OnCubeAmountChanged(int amount)
        {
            if (amount != 0)
                return;
            
            End();
        }
    }
}
