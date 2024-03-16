using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Gameplay.PlayerData.Models;
using Codebase.Gameplay.PlayerData.Services.Implementations;
using Codebase.Gameplay.PlayerData.Services.Interfaces;

namespace Codebase.Gameplay.PlayerData.CreationServices
{
    public class GameplayPlayerCreationService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;

        public GameplayPlayerCreationService(IEntityRepository entityRepository, IIdGenerator idGenerator)
        {
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
        }

        public IGameplayPlayerDataService Create()
        {
            int id = _idGenerator.Generate();

            GameplayPlayerDataService gameplayPlayerDataService = new GameplayPlayerDataService(_entityRepository, id);

            GameplayPlayerDataModel gameplayPlayerDataModel = new GameplayPlayerDataModel(id, BallConstants.DefaultAmountToShoot);
            _entityRepository.Register(gameplayPlayerDataModel);

            return gameplayPlayerDataService;
        }
    }
}
