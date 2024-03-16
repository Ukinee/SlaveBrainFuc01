using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.PauseServices;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.Game.Services.Implementations;
using Codebase.Gameplay.Interface.Presentation.Implementations;
using Codebase.Gameplay.Interface.Services.Interfaces;
using Codebase.Gameplay.Interface.Views.Implementations;

namespace Codebase.Gameplay.Interface.Services.Implementations.CreationServices
{
    public class GameplayWinFormCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly AssetProvider _assetProvider;
        private readonly IInterfaceService _interfaceService;
        private readonly IAudioService _audioService;
        private readonly PauseService _pauseService;
        private readonly GameService _gameService;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly string _path;
        private IWinFormService _winFormService;

        public GameplayWinFormCreationService
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            AssetProvider assetProvider,
            IInterfaceService interfaceService,
            IAudioService audioService,
            FilePathProvider filePathProvider,
            PauseService pauseService,
            GameService gameService,
            IWinFormService winFormService
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _assetProvider = assetProvider;
            _interfaceService = interfaceService;
            _audioService = audioService;
            _pauseService = pauseService;
            _gameService = gameService;
            _winFormService = winFormService;
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
            _path = filePathProvider.Forms.Data[PathConstants.Forms.GameplayWinForm];
        }

        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            SimpleForm form = new SimpleForm(false, id);
            _entityRepository.Register(form);

            WinFormView view = _assetProvider.Instantiate<WinFormView>(_path);

            WinFormPresenter winFormPresenter = new WinFormPresenter(id, view, _winFormService, new DisposeCommand(_entityRepository));
            FormVisibilityPresenter formVisibilityPresenter = new FormVisibilityPresenter(id, _getFormVisibilityQuery, view);

            view.Construct(winFormPresenter);
            
            winFormPresenter.Enable();
            formVisibilityPresenter.Enable();

            return new Tuple<FormBase, IFormView>(form, view);
        }
    }
}
