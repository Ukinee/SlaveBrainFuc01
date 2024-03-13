using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.Common;
using Codebase.Forms.CQRS;
using Codebase.Forms.Factories;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Forms.Services.Interfaces;
using Codebase.Forms.Views.Implementations;
using Codebase.Game.CQRS.Commands;
using Codebase.Game.Services.Implementations;
using Codebase.Game.Services.Implementations.Repositories;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Services.Implementations;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class MainMenuSceneFactory : ISceneStateFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IAudioService _audioService;
        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;
        private IPlayerIdProvider _playerIdProvider;

        public MainMenuSceneFactory
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IAudioService audioService,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IPlayerIdProvider playerIdProvider
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
            _playerIdProvider = playerIdProvider;
        }

        public ISceneState CreateSceneState
            (IStateMachineService<IScenePayload> stateMachineService, IScenePayload scenePayload)
        {
            #region Configs

            string[] availableLevelIds = new[]
            {
                "Test Two Towers",
                "Test Three Towers",
            };

            #endregion

            SetFormVisibilityCommand setFormVisibilityCommand = new SetFormVisibilityCommand(_entityRepository);
            InterfaceService interfaceService = new InterfaceService(setFormVisibilityCommand);

            string path = _filePathProvider.Forms.Data[PathConstants.Forms.Interface];
            InterfaceView interfaceView = _assetProvider.Instantiate<InterfaceView>(path);

            SetLevelSelectionCommand setLevelSelectionCommand = new SetLevelSelectionCommand(_entityRepository);
            LevelRepositoryController levelRepositoryController = new LevelRepositoryController();
            SelectedLevelService selectedLevelService = new SelectedLevelService(setLevelSelectionCommand);
            GetPassedLevelsQuery getPassedLevelsQuery = new GetPassedLevelsQuery(_playerIdProvider, _entityRepository);

            FormCreationServiceFactory formCreationServiceFactory = new FormCreationServiceFactory
            (
                _idGenerator,
                _entityRepository,
                interfaceService,
                levelRepositoryController,
                interfaceView,
                _audioService,
                selectedLevelService,
                _assetProvider,
                _filePathProvider,
                getPassedLevelsQuery,
                _playerIdProvider,
                stateMachineService
            );

            FormCreationService formCreationService = formCreationServiceFactory.Create(availableLevelIds);

            MainMenuFactory mainMenuFactory = new MainMenuFactory(formCreationService);

            return new MainMenuScene(mainMenuFactory);
        }
    }
}
