using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Game.CQRS.Queries;
using Codebase.Game.Models;
using Codebase.Game.Presentations.Implementations;
using Codebase.Game.Services.Implementations.Repositories;
using Codebase.Game.Services.Interfaces;
using Codebase.Game.Views.Implementations;

namespace Codebase.Game.Services.Implementations.CreationServices
{
    public class LevelCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly AssetProvider _assetProvider;
        private readonly IEntityRepository _entityRepository;
        private readonly LevelRepositoryController _levelRepositoryController;
        private readonly ISelectedLevelService _selectedLevelService;
        private readonly GetLevelSelectionQuery _getLevelSelectionQuery;
        private readonly GetLevelStateQuery _getLevelStateQuery;
        private readonly GetLevelIdQuery _getLevelIdQuery;
        private readonly string _assetPath;

        public LevelCreationService
        (
            IIdGenerator idGenerator,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IEntityRepository entityRepository,
            LevelRepositoryController levelRepositoryController,
            ISelectedLevelService selectedLevelService
        )
        {
            _idGenerator = idGenerator;
            _assetProvider = assetProvider;
            _entityRepository = entityRepository;
            _levelRepositoryController = levelRepositoryController;
            _selectedLevelService = selectedLevelService;
            _assetPath = filePathProvider.Forms.Data[PathConstants.Forms.LevelView];
            _getLevelStateQuery = new GetLevelStateQuery(entityRepository);
            _getLevelSelectionQuery = new GetLevelSelectionQuery(entityRepository);
            _getLevelIdQuery = new GetLevelIdQuery(entityRepository);
        }

        public int Create(string levelId, bool isPassed)
        {
            int id = _idGenerator.Generate();

            LevelModel model = new LevelModel(id, levelId);
            _entityRepository.Register(model);

            LevelView view = _assetProvider.Instantiate<LevelView>(_assetPath);

            LevelPresenter presenter = new LevelPresenter
            (
                id,
                _getLevelSelectionQuery,
                _getLevelStateQuery,
                _getLevelIdQuery,
                view,
                _selectedLevelService
            );

            _levelRepositoryController.Register(model, view);

            view.Construct(presenter);
            model.SetPassed(isPassed);
            presenter.Enable();

            return id;
        }
    }
}
