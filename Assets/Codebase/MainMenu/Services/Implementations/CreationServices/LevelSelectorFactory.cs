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
using Codebase.Game.CQRS.Queries;
using Codebase.MainMenu.Models;
using Codebase.MainMenu.Presentations.Implementations;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Implementations;

namespace Codebase.MainMenu.Services.Implementations.CreationServices
{
    public class LevelSelectorFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly GetLevelIdsQuery _getLevelIdsQuery;
        private readonly GetLevelIdQuery _getLevelIdQuery;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly ILevelViewRepository _levelViewRepository;
        private readonly IInterfaceService _interfaceService;
        private readonly IInterfaceView _interfaceView;
        private readonly ISelectedLevelService _selectedLevelService;
        private readonly IStateMachineService<IScenePayload> _stateMachineService;
        private readonly string _assetPath;

        public LevelSelectorFactory
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            ILevelViewRepository levelViewRepository,
            IInterfaceService interfaceService,
            IInterfaceView interfaceView,
            ISelectedLevelService selectedLevelService,
            IStateMachineService<IScenePayload> stateMachineService
        )
        {
            _assetProvider = assetProvider;
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _levelViewRepository = levelViewRepository;
            _interfaceService = interfaceService;
            _interfaceView = interfaceView;
            _selectedLevelService = selectedLevelService;
            _stateMachineService = stateMachineService;
            _getLevelIdsQuery = new GetLevelIdsQuery(entityRepository);
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
            _getLevelIdQuery = new GetLevelIdQuery(entityRepository);
            _assetPath = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuLevelSelectingFormView];
        }
        
        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            LevelSelectionFormModel model = new LevelSelectionFormModel(true, id); //todo: debug tru
            _entityRepository.Register(model);
            
            LevelSelectorFormView view = _assetProvider.Instantiate<LevelSelectorFormView>(_assetPath);

            FormVisibilityPresenter visibilityPresenter = new FormVisibilityPresenter
            (
                id,
                _getFormVisibilityQuery,
                view
            );

            LevelSelectingFormPresenter levelSelectingFormPresenter = new LevelSelectingFormPresenter
            (
                id,
                new DisposeCommand(_entityRepository),
                _getLevelIdsQuery,
                _getLevelIdQuery,
                _levelViewRepository,
                view,
                _interfaceService,
                _selectedLevelService,
                _stateMachineService
            );
            
            _interfaceView.SetChild(view);
            
            visibilityPresenter.Enable();
            levelSelectingFormPresenter.Enable();
            
            view.Construct(levelSelectingFormPresenter);
            
            return new Tuple<FormBase, IFormView>(model, view);
        }
    }
}
