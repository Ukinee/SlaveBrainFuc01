using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Models;
using Codebase.MainMenu.Presentations.Implementations;
using Codebase.MainMenu.Presentations.Implementations.LevelsSelectors;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Implementations;

namespace Codebase.MainMenu.Services.Implementations.CreationServices
{
    public class LevelSelectorFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly GetLevelIdsQuery _getLevelIdsQuery;
        private readonly GetMapIdsQuery _getMapIdsQuery;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly ILevelViewRepository _levelViewRepository;
        private readonly IMapViewRepository _mapViewRepository;
        private readonly IInterfaceService _interfaceService;
        private readonly IInterfaceView _interfaceView;
        private readonly string _assetPath;
        private IMainMenuLevelChanger _mainMenuLevelChanger;

        public LevelSelectorFactory
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            ILevelViewRepository levelViewRepository,
            IMapViewRepository mapViewRepository,
            IInterfaceService interfaceService,
            IInterfaceView interfaceView,
            IMainMenuLevelChanger mainMenuLevelChanger
        )
        {
            _assetProvider = assetProvider;
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _levelViewRepository = levelViewRepository;
            _mapViewRepository = mapViewRepository;
            _interfaceService = interfaceService;
            _interfaceView = interfaceView;
            _mainMenuLevelChanger = mainMenuLevelChanger;
            _getLevelIdsQuery = new GetLevelIdsQuery(entityRepository);
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
            _getMapIdsQuery = new GetMapIdsQuery(entityRepository);
            _assetPath = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuLevelSelectingFormView];
        }
        
        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            MainMenuLevelSelectorFormModel model = new MainMenuLevelSelectorFormModel(true, id); //todo: debug tru
            _entityRepository.Register(model);
            
            LevelSelectorFormView view = _assetProvider.Instantiate<LevelSelectorFormView>(_assetPath);

            FormVisibilityPresenter visibilityPresenter = new FormVisibilityPresenter
            (
                id,
                _getFormVisibilityQuery,
                view
            );

            LevelSelectorFormPresenter levelSelectorFormPresenter = new LevelSelectorFormPresenter
            (
                id,
                new DisposeCommand(_entityRepository),
                _getLevelIdsQuery,
                _getMapIdsQuery,
                _levelViewRepository,
                _mapViewRepository,
                view,
                _interfaceService,
                _mainMenuLevelChanger
            );
            
            _interfaceView.SetChild(view);
            
            visibilityPresenter.Enable();
            levelSelectorFormPresenter.Enable();
            
            view.Construct(levelSelectorFormPresenter);
            
            return new Tuple<FormBase, IFormView>(model, view);
        }
    }
}
