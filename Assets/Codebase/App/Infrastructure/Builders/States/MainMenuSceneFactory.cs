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
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Forms.Services.Interfaces;
using Codebase.Forms.Views.Implementations;

namespace Codebase.App.Infrastructure.Builders.States
{
    public class MainMenuSceneFactory : ISceneStateFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IAudioService _audioService;
        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;

        public MainMenuSceneFactory
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IAudioService audioService,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
        }

        public ISceneState CreateSceneState(IStateMachineService<IScenePayload> stateMachineService)
        {
            SetFormVisibilityCommand setFormVisibilityCommand = new SetFormVisibilityCommand(_entityRepository);
            InterfaceService interfaceService = new InterfaceService(setFormVisibilityCommand);

            string path = _filePathProvider.Forms.Data[PathConstants.Forms.Interface];
            InterfaceView interfaceView = _assetProvider.Instantiate<InterfaceView>(path);

            FormCreationServiceFactory formCreationServiceFactory = new FormCreationServiceFactory
            (
                _idGenerator,
                _entityRepository,
                interfaceService,
                interfaceView,
                _audioService,
                _assetProvider,
                _filePathProvider
            );
            
            FormCreationService formCreationService = formCreationServiceFactory.Create();

            MainMenuFactory mainMenuFactory = new MainMenuFactory(formCreationService);

            return new MainMenuScene(mainMenuFactory);
        }
    }
}
