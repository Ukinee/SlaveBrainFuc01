using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Services.Common;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.MainMenu.Presentations.Implementations;
using Codebase.MainMenu.Views.Implementations;

namespace Codebase.MainMenu.Factories
{
    public class MainMenuSettingsFormFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IInterfaceService _interfaceService;
        private readonly IEntityRepository _entityRepository;
        private readonly IAudioService _audioService;
        private readonly AssetProvider _assetProvider;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly string _path;

        public MainMenuSettingsFormFactory
        (
            IIdGenerator idGenerator,
            IInterfaceService interfaceService,
            IEntityRepository entityRepository,
            IAudioService audioService,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _idGenerator = idGenerator;
            _interfaceService = interfaceService;
            _entityRepository = entityRepository;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
            _path = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuSettingsForm];
        }

        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            MainMenuSettingsFormView view = _assetProvider.Instantiate<MainMenuSettingsFormView>(_path);
            SimpleForm model = new SimpleForm(false, id);
            _entityRepository.Register(model);
            
            MainMenuSettingsFormPresenter presenter = new MainMenuSettingsFormPresenter(id, _interfaceService, _audioService, view, new DisposeCommand(_entityRepository));
            FormVisibilityPresenter formVisibilityPresenter = new FormVisibilityPresenter(id, _getFormVisibilityQuery, view);

            formVisibilityPresenter.Enable();
            view.Construct(presenter);

            return new Tuple<FormBase, IFormView>(model, view);
        }
    }
}
