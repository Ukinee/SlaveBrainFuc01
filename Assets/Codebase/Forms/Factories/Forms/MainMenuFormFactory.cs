using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Presentations.Implementations.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Implementations;
using Codebase.Forms.Views.Implementations.MainMenu;
using Codebase.Forms.Views.Interfaces;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Presentations.Implementations;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.Forms.Factories.Forms
{
    public class MainMenuFormFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly IIdGenerator _idGenerator;
        private readonly IInterfaceService _interfaceService;
        private readonly IEntityRepository _entityRepository;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly string _path;
        private IPlayerIdProvider _playerIdProvider;

        public MainMenuFormFactory
        (
            IIdGenerator idGenerator,
            IInterfaceService interfaceService,
            IEntityRepository entityRepository,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IPlayerIdProvider playerIdProvider
        )
        {
            _assetProvider = assetProvider;
            _playerIdProvider = playerIdProvider;
            _idGenerator = idGenerator;
            _interfaceService = interfaceService;
            _entityRepository = entityRepository;
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);

            _path = filePathProvider.Forms.Data[PathConstants.Forms.MainMenuForm];
        }

        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            MainMenuFormView view = _assetProvider.Instantiate<MainMenuFormView>(_path);
            SimpleForm model = new SimpleForm(true, id);
            _entityRepository.Register(model);

            MainMenuFormPresenter formPresenter = new MainMenuFormPresenter(id, _interfaceService, new DisposeCommand(_entityRepository));
            FormVisibilityPresenter formVisibilityPresenter = new FormVisibilityPresenter(id, _getFormVisibilityQuery, view);

            GetCoinAmountQuery getCoinAmountQuery = new GetCoinAmountQuery(_playerIdProvider, _entityRepository);

            CoinAmountPresenter coinAmountPresenter = new CoinAmountPresenter(getCoinAmountQuery, view);

            coinAmountPresenter.Enable();
            formVisibilityPresenter.Enable();
            view.Construct(formPresenter);

            return new Tuple<FormBase, IFormView>(model, view);
        }
    }
}
