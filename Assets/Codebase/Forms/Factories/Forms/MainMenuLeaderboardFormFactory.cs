using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Presentations.Implementations.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Implementations.MainMenu;
using Codebase.Forms.Views.Interfaces;

namespace Codebase.Forms.Factories.Forms
{
    public class MainMenuLeaderboardFormFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IInterfaceService _interfaceService;
        private readonly IEntityRepository _entityRepository;
        private readonly AssetProvider _assetProvider;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly string _path;

        public MainMenuLeaderboardFormFactory
        (
            IIdGenerator idGenerator,
            IInterfaceService interfaceService,
            IEntityRepository entityRepository,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _idGenerator = idGenerator;
            _interfaceService = interfaceService;
            _entityRepository = entityRepository;
            _assetProvider = assetProvider;
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
            _path = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuLeaderboardForm];
        }

        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            MainMenuLeaderboardFormView view = _assetProvider.Instantiate<MainMenuLeaderboardFormView>(_path);
            SimpleForm model = new SimpleForm(false, id);
            _entityRepository.Register(model);

            MainMenuLeaderboardFormPresenter formPresenter = new MainMenuLeaderboardFormPresenter(_interfaceService);
            FormVisibilityPresenter formVisibilityPresenter = new FormVisibilityPresenter(id, _getFormVisibilityQuery, view);

            formVisibilityPresenter.Enable();
            view.Construct(formPresenter);

            return new Tuple<FormBase, IFormView>(model, view);
        }
    }
}
