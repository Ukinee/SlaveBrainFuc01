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
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Services.Implementations.CreationServices
{
    public class MainMenuMapCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly AssetProvider _assetProvider;
        private readonly IEntityRepository _entityRepository;
        private readonly MapRepositoryController _mapRepositoryController;
        private readonly ISelectedMapService _selectedMapService;
        private readonly IShopService _shopService;
        private readonly MainMenuMapGetTypeQuery _mainMenuMapGetTypeQuery;
        private readonly MainMenuMapGetPriceQuery _mainMenuMapGetPriceQuery;
        private readonly GetMapSelectionQuery _getMapSelectionQuery;
        private readonly GetMapBoughtQuery _getMapBoughtQuery;
        private readonly DisposeCommand _disposeCommand;
        private readonly string _assetPath;

        public MainMenuMapCreationService
        (
            IIdGenerator idGenerator,
            AssetProvider assetProvider,
            IEntityRepository entityRepository,
            MapRepositoryController mapRepositoryController,
            ISelectedMapService selectedMapService,
            IShopService shopService,
            FilePathProvider filePathProvider
        )
        {
            _idGenerator = idGenerator;
            _assetProvider = assetProvider;
            _entityRepository = entityRepository;
            _mapRepositoryController = mapRepositoryController;
            _selectedMapService = selectedMapService;
            _shopService = shopService;
            _mainMenuMapGetTypeQuery = new MainMenuMapGetTypeQuery(_entityRepository);
            _mainMenuMapGetPriceQuery = new MainMenuMapGetPriceQuery(_entityRepository);
            _getMapSelectionQuery = new GetMapSelectionQuery(_entityRepository);
            _disposeCommand = new DisposeCommand(_entityRepository);
            _getMapBoughtQuery = new GetMapBoughtQuery(_entityRepository);
            _assetPath = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuMapView];
        }

        public int Create(MapType mapType, int price, bool isSelected, bool isBought)
        {
            int id = _idGenerator.Generate();
            
            MainMenuMapModel model = new MainMenuMapModel(id, mapType, price, isSelected, isBought);
            _entityRepository.Register(model);

            MainMenuMapView view = _assetProvider.Instantiate<MainMenuMapView>(_assetPath);
            _mapRepositoryController.Register(model, view);

            MainMenuMapPresenter presenter = new MainMenuMapPresenter
            (
                id,
                _getMapSelectionQuery,
                _getMapBoughtQuery,
                _mainMenuMapGetTypeQuery,
                _mainMenuMapGetPriceQuery,
                view,
                _selectedMapService,
                _shopService,
                _disposeCommand
            );
             
            view.Construct(presenter);
            presenter.Enable();
            
            return id;
        }
    }
}
