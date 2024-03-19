using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Models;
using Codebase.MainMenu.Presentations.Implementations.LevelsSelectors;
using Codebase.MainMenu.Services.Implementations.Repositories;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Implementations;

namespace Codebase.MainMenu.Services.Implementations.CreationServices
{
    public class LevelCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly AssetProvider _assetProvider;
        private readonly IEntityRepository _entityRepository;
        private readonly LevelRepositoryController _levelRepositoryController;
        private readonly ISelectedLevelService _selectedLevelService;
        private readonly IShopService _shopService;
        private readonly GetLevelSelectionQuery _getLevelSelectionQuery;
        private readonly GetLevelPassStatusQuery _getLevelPassStatusQuery;

        private readonly GetLevelIdQuery _getLevelIdQuery;
        private readonly string _assetPath;
        private GetLevelUnlockStatusQuery _getLevelUnlockStatusQuery;
        private GetLevelPriceQuery _getLevelPriceQuery;

        public LevelCreationService
        (
            IIdGenerator idGenerator,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IEntityRepository entityRepository,
            LevelRepositoryController levelRepositoryController,
            ISelectedLevelService selectedLevelService,
            IShopService shopService
        )
        {
            _idGenerator = idGenerator;
            _assetProvider = assetProvider;
            _entityRepository = entityRepository;
            _levelRepositoryController = levelRepositoryController;
            _selectedLevelService = selectedLevelService;
            _shopService = shopService;
            _assetPath = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuLevelView];
            _getLevelPassStatusQuery = new GetLevelPassStatusQuery(entityRepository);
            _getLevelSelectionQuery = new GetLevelSelectionQuery(entityRepository);
            _getLevelIdQuery = new GetLevelIdQuery(entityRepository);
            _getLevelUnlockStatusQuery = new GetLevelUnlockStatusQuery(entityRepository);
            _getLevelPriceQuery = new GetLevelPriceQuery(entityRepository);
        }

        public int Create(string levelId, bool isPassed, bool isUnlocked, int price)
        {
            int id = _idGenerator.Generate();

            LevelModel model = new LevelModel(id, levelId, price, isUnlocked, false, isPassed);
            _entityRepository.Register(model);

            LevelView view = _assetProvider.Instantiate<LevelView>(_assetPath);

            LevelPresenter presenter = new LevelPresenter
            (
                id,
                new DisposeCommand(_entityRepository),
                _getLevelUnlockStatusQuery,
                _getLevelPriceQuery,
                _getLevelSelectionQuery,
                _getLevelPassStatusQuery,
                _getLevelIdQuery,
                _shopService,
                view,
                _selectedLevelService
            );

            _levelRepositoryController.Register(model, view);

            view.Construct(presenter);
            presenter.Enable();

            return id;
        }
    }
}
