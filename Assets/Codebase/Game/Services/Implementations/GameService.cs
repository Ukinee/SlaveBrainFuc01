using System;
using System.Threading;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.PauseServices;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations;
using Codebase.Gameplay.Interface.Services.Interfaces;
using Codebase.Gameplay.PlayerData.CQRS.Queries;
using Codebase.Maps.Common;
using Codebase.PlayerData.CQRS.Commands;
using Codebase.PlayerData.Services.Interfaces;
using Cysharp.Threading.Tasks;

namespace Codebase.Game.Services.Implementations
{
    public class GameService
    {
        private readonly GameStarter _gameStarter;
        private readonly GameEnder _gameEnder;
        private readonly AddPassedLevelCommand _addPassedLevelCommand;
        private readonly AddPlayerCoinsCommand _addPlayerCoinsCommand;
        private readonly GetGameplayPlayerCoinAmountQuery _getGameplayPlayerCoinAmountQuery;
        private readonly CubeRepositoryController _cubeRepositoryController;
        private readonly IWinFormService _winFormService;
        private readonly IStateMachineService<IScenePayload> _stateMachineService;
        private readonly IDataService _dataService;
        private readonly PauseService _pauseService;

        private string _levelId;

        private CancellationTokenSource _cancellationTokenSource;
        private UniTask _currentTask = UniTask.CompletedTask;

        public GameService
        (
            PauseService pauseService,
            GameStarter gameStarter,
            GameEnder gameEnder,
            AddPassedLevelCommand addPassedLevelCommand,
            AddPlayerCoinsCommand addPlayerCoinsCommand,
            GetGameplayPlayerCoinAmountQuery getGameplayPlayerCoinAmountQuery,
            CubeRepositoryController cubeRepositoryController,
            IWinFormService winFormService,
            IStateMachineService<IScenePayload> stateMachineService,
            IDataService dataService
        )
        {
            _pauseService = pauseService;
            _gameStarter = gameStarter;
            _gameEnder = gameEnder;
            _addPassedLevelCommand = addPassedLevelCommand;
            _addPlayerCoinsCommand = addPlayerCoinsCommand;
            _getGameplayPlayerCoinAmountQuery = getGameplayPlayerCoinAmountQuery;
            _cubeRepositoryController = cubeRepositoryController;
            _winFormService = winFormService;
            _stateMachineService = stateMachineService;
            _dataService = dataService;
        }

        public void Start(string levelId, MapType mapType)
        {
            if (string.IsNullOrEmpty(_levelId) == false)
                throw new Exception("GameService: level id already set.");

            _levelId = levelId;

            _cubeRepositoryController.OnCubeAmountChanged += OnCubeAmountChanged;
            _gameStarter.Start(levelId, mapType);
        }

        public void End()
        {
            _cubeRepositoryController.OnCubeAmountChanged -= OnCubeAmountChanged;
            _dataService.Save();
            _gameEnder.End();

            if (_pauseService.IsPaused)
            {
                "Unusual behavior. Unpause application".Log();
                _pauseService.ResumeApplication();
            }

            if (_currentTask.Status == UniTaskStatus.Pending)
                _cancellationTokenSource.Cancel();
            
            _stateMachineService.SetState(new MainMenuScenePayload());
        }

        private async UniTask EndAsWin(CancellationToken cancellationToken = default)
        {
            try
            {
                _addPassedLevelCommand.Handle(_levelId);
                int coins = _getGameplayPlayerCoinAmountQuery.Handle().Value;
                _addPlayerCoinsCommand.Handle(coins);

                await UniTask.Delay(1000, cancellationToken: cancellationToken); //todo : hardcoded value
                
                _winFormService.Enable(coins);
                
                await UniTask.WaitUntil(_winFormService.GetContinueClicked, cancellationToken: cancellationToken);

                End();
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void OnCubeAmountChanged(int amount)
        {
            if (amount != 0)
                return;

            _cancellationTokenSource = new CancellationTokenSource();
            _currentTask = EndAsWin(_cancellationTokenSource.Token);
        }
    }
}
