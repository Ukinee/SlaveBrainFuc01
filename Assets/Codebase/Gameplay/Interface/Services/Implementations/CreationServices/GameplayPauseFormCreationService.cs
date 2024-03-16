using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.PauseServices;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.Game.Services.Implementations;
using Codebase.Gameplay.Interface.Presentation.Implementations;
using Codebase.Gameplay.Interface.Views.Implementations;

namespace Codebase.Gameplay.Interface.Services.Implementations.CreationServices
{
    public class GameplayPauseFormCreationService
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

        public GameplayPauseFormCreationService
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            AssetProvider assetProvider,
            IInterfaceService interfaceService,
            IAudioService audioService,
            FilePathProvider filePathProvider,
            PauseService pauseService,
            GameService gameService
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _assetProvider = assetProvider;
            _interfaceService = interfaceService;
            _audioService = audioService;
            _pauseService = pauseService;
            _gameService = gameService;
            _path = filePathProvider.Forms.Data[PathConstants.Forms.GameplayPauseForm];
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
        }

        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            SimpleForm simpleForm = new SimpleForm(false, id);
            _entityRepository.Register(simpleForm);

            PauseFormView pauseFormView = _assetProvider.Instantiate<PauseFormView>(_path);

            PauseFormPresenter pauseFormPresenter = new PauseFormPresenter
            (
                id,
                _pauseService,
                _gameService,
                _audioService,
                _interfaceService,
                pauseFormView,
                new DisposeCommand(_entityRepository)
            );

            FormVisibilityPresenter formVisibilityPresenter = new FormVisibilityPresenter
            (
                id,
                _getFormVisibilityQuery,
                pauseFormView
            );

            pauseFormView.Construct(pauseFormPresenter);

            pauseFormPresenter.Enable();
            formVisibilityPresenter.Enable();

            return new Tuple<FormBase, IFormView>(simpleForm, pauseFormView);
        }
    }
}
