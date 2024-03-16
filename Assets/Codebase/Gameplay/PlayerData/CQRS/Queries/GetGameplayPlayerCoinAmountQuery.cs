using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.Models;
using Codebase.Gameplay.PlayerData.Services.Interfaces;

namespace Codebase.Gameplay.PlayerData.CQRS.Queries
{
    public class GetGameplayPlayerCoinAmountQuery : EntityUseCaseBase<GameplayPlayerDataModel>
    {
        private readonly IGameplayPlayerDataService _gameplayPlayerDataService;

        public GetGameplayPlayerCoinAmountQuery(IGameplayPlayerDataService gameplayPlayerDataService, IEntityRepository repository) : base(repository)
        {
            _gameplayPlayerDataService = gameplayPlayerDataService;
        }

        public ILiveData<int> Handle() =>
            Get(_gameplayPlayerDataService.Id).CoinAmount;
    }
}
