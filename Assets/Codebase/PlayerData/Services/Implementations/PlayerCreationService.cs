using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using Codebase.PlayerData.Infrastructure.DTO;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.Services.Implementations
{
    public class PlayerCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IDataService _dataService;

        public PlayerCreationService
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IDataService dataService
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _dataService = dataService;
        }

        public int Create()
        {
            PlayerDataObject dataObject = _dataService.Get();
            int id = _idGenerator.Generate();
            
            PlayerDataModel playerDataModel = new PlayerDataModel
            (
                id,
                dataObject.Coins,
                dataObject.LevelsPassed,
                dataObject.SelectedMap,
                dataObject.PassedLevels,
                dataObject.UnlockedStructuresForInfiniteLevels,
                dataObject.UnlockedMaps
            );
            
            _entityRepository.Register(playerDataModel);
            
            return id;
        }
    }
}
