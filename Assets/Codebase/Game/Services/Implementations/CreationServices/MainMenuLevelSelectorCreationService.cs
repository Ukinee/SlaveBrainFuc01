using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Forms.Models;
using Codebase.Forms.Views.Interfaces;
using Codebase.Game.CQRS.Commands;
using Codebase.PlayerData.CQRS.Queries;

namespace Codebase.Game.Services.Implementations.CreationServices
{
    public class MainMenuLevelSelectorCreationService
    {
        private readonly LevelCreationService _levelCreationService;
        private readonly LevelSelectorFactory _levelSelectorFactory;
        private readonly GetPassedLevelsQuery _getPassedLevelsQuery;
        private readonly string[] _levelIds;
        private readonly AddLevelToLevelSelectionFormCommand _addLevelToLevelSelectionFormCommand;

        public MainMenuLevelSelectorCreationService
        (
            LevelCreationService levelCreationService,
            LevelSelectorFactory levelSelectorFactory,
            GetPassedLevelsQuery getPassedLevelsQuery,
            IEntityRepository entityRepository,
            string[] levelIds
        )
        {
            _levelCreationService = levelCreationService;
            _levelSelectorFactory = levelSelectorFactory;
            _getPassedLevelsQuery = getPassedLevelsQuery;
            _levelIds = levelIds;
            _addLevelToLevelSelectionFormCommand = new AddLevelToLevelSelectionFormCommand(entityRepository);
        }

        public Tuple<FormBase, IFormView> Create()
        {
            Tuple<FormBase, IFormView> tuple = _levelSelectorFactory.Create();

            int selectorFormId = tuple.Item1.Id;
            IReadOnlyList<string> passedLevels = _getPassedLevelsQuery.Handle();

            foreach (string levelStringId in _levelIds)
            {
                int levelId = _levelCreationService.Create(levelStringId, passedLevels.Contains(levelStringId));
                
                _addLevelToLevelSelectionFormCommand.Handle(selectorFormId, levelId);
            }

            return tuple;
        }
    }
}
