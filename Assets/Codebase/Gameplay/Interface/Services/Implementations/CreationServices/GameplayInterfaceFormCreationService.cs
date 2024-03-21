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
using Codebase.Gameplay.PlayerData.CQRS.Queries;
using Codebase.Gameplay.Shooting.CQRS.Queries;

namespace Codebase.Gameplay.Interface.Services.Implementations.CreationServices
{
    public class GameplayInterfaceFormCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly AssetProvider _assetProvider;
        private readonly IInterfaceService _interfaceService;
        private readonly IAudioService _audioService;
        private readonly PauseService _pauseService;
        private readonly GameService _gameService;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly GetGameplayPlayerCoinAmountQuery _coinAmountQuery;
        private readonly string _path;
        private GetUpgradePointsQuery _upgradePointsQuery;
        private GetMaxUpgradePointsQuery _maxUpgradePointsQuery;
        private GetBallsToShootQuery _ballsToShootQuery;

        public GameplayInterfaceFormCreationService
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            AssetProvider assetProvider,
            IInterfaceService interfaceService,
            IAudioService audioService,
            FilePathProvider filePathProvider,
            PauseService pauseService,
            GameService gameService,
            GetGameplayPlayerCoinAmountQuery coinAmountQuery,
            GetBallsToShootQuery ballsToShootQuery,
            GetUpgradePointsQuery upgradePointsQuery,
            GetMaxUpgradePointsQuery maxUpgradePointsQuery
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _assetProvider = assetProvider;
            _interfaceService = interfaceService;
            _audioService = audioService;
            _pauseService = pauseService;
            _gameService = gameService;
            _coinAmountQuery = coinAmountQuery;
            _ballsToShootQuery = ballsToShootQuery;
            _upgradePointsQuery = upgradePointsQuery;
            _maxUpgradePointsQuery = maxUpgradePointsQuery;
            _getFormVisibilityQuery = new GetFormVisibilityQuery(entityRepository);
            _path = filePathProvider.Forms.Data[PathConstants.Forms.GameplayForm];
        }

        public Tuple<FormBase, IFormView> Create()
        {
            int id = _idGenerator.Generate();

            SimpleForm form = new SimpleForm(true, id);
            _entityRepository.Register(form);

            GameplayInterfaceFormView view = _assetProvider.Instantiate<GameplayInterfaceFormView>(_path);

            GameplayInterfaceFormPresenter winFormPresenter = new GameplayInterfaceFormPresenter
            (
                id,
                _pauseService,
                _coinAmountQuery,
                _ballsToShootQuery,
                _upgradePointsQuery,
                _maxUpgradePointsQuery,
                _interfaceService,
                view,
                new DisposeCommand(_entityRepository)
            );

            FormVisibilityPresenter formVisibilityPresenter = new FormVisibilityPresenter(id, _getFormVisibilityQuery, view);

            view.Construct(winFormPresenter);

            winFormPresenter.Enable();
            formVisibilityPresenter.Enable();

            return new Tuple<FormBase, IFormView>(form, view);
        }
    }
}
