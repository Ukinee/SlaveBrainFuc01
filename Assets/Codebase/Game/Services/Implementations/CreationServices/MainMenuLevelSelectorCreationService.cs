using System.Collections.Generic;
using System.Linq;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Game.CQRS.Commands;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.Game.Services.Implementations.CreationServices
{
    public class MainMenuLevelSelectorCreationService
    {
        private readonly LevelCreationService _levelCreationService;
        private readonly LevelSelectorCreationService _levelSelectorCreationService;
        private readonly GetPassedLevelsQuery _getPassedLevelsQuery;
        private readonly AddLevelToLevelSelectionFormCommand _addLevelToLevelSelectionFormCommand;

        public MainMenuLevelSelectorCreationService
        (
            LevelCreationService levelCreationService,
            LevelSelectorCreationService levelSelectorCreationService,
            GetPassedLevelsQuery getPassedLevelsQuery,
            IEntityRepository entityRepository
        )
        {
            _levelCreationService = levelCreationService;
            _levelSelectorCreationService = levelSelectorCreationService;
            _getPassedLevelsQuery = getPassedLevelsQuery;
            _addLevelToLevelSelectionFormCommand = new AddLevelToLevelSelectionFormCommand(entityRepository);
        }

        public void Create(string[] levelIds)
        {
            int selectorForm = _levelSelectorCreationService.Create();
            IReadOnlyList<string> passedLevels = _getPassedLevelsQuery.Handle();

            foreach (string levelStringId in levelIds)
            {
                int levelId = _levelCreationService.Create(levelStringId, passedLevels.Contains(levelStringId));
                
                _addLevelToLevelSelectionFormCommand.Handle(selectorForm, levelId);
            }
        }
    }
}
