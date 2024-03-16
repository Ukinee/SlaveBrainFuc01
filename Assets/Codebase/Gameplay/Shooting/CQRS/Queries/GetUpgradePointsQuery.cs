using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.Models;
using Codebase.Gameplay.PlayerData.Services.Interfaces;

namespace Codebase.Gameplay.Shooting.CQRS.Queries
{
    public class GetUpgradePointsQuery : EntityUseCaseBase<GameplayPlayerDataModel>
    {
        private readonly IGameplayPlayerDataService _playerDataService;

        public GetUpgradePointsQuery(IGameplayPlayerDataService playerDataService, IEntityRepository repository) : base(repository)
        {
            _playerDataService = playerDataService;
        }

        public ILiveData<int> Handle() =>
            Get(_playerDataService.Id).UpgradePoints;
    }
}
