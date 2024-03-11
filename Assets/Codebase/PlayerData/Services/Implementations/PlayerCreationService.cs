using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Forms.Views.Interfaces;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Infrastructure.DTO;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Presentations.Implementations;
using Codebase.PlayerData.Services.Interfaces;
using Codebase.PlayerData.Views.Implementations;

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
                dataObject.PassedLevels,
                dataObject.UnlockedStructuresForInfiniteLevels,
                dataObject.UnlockedMaps
            );
            
            _entityRepository.Register(playerDataModel);
            
            return id;
        }
    }
}
