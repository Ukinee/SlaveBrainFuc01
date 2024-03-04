using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Frameworks.SignalSystem.Common.SignalActions;
using Codebase.Cubes.Controllers.Actions;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Implementations;
using Codebase.Structures.Controllers.Actions;
using Codebase.Structures.Infrastructure.Services.Implementations;
using Codebase.Structures.Services.Implementations;

namespace Codebase.Structures.Controllers
{
    public class StructureSignalControllerFactory
    {
        private AssetProvider _assetProvider;
        private FilePathProvider _filePathProvider;
        private IIdGenerator _idGenerator;
        private ISignalBus _signalBus;
        private IEntityRepository _entityRepository;
        private CubeViewPool _cubeViewPool;

        public StructureSignalControllerFactory
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IIdGenerator idGenerator,
            ISignalBus signalBus,
            IEntityRepository entityRepository,
            CubeViewPool cubeViewPool
        )
        {
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
            _idGenerator = idGenerator;
            _signalBus = signalBus;
            _entityRepository = entityRepository;
            _cubeViewPool = cubeViewPool;
        }

        public StructureSignalController Create()
        {
            CubeViewRepository cubeViewRepository = new CubeViewRepository();

            CubeRepositoryController cubeRepositoryController = new CubeRepositoryController(cubeViewRepository);

            StructureReader structureReader = new StructureReader(_assetProvider, _filePathProvider);
            StructureViewFactory structureViewFactory = new StructureViewFactory(_assetProvider, _filePathProvider);

            CubeCreationService cubeCreationService = new CubeCreationService
            (
                _signalBus,
                _idGenerator,
                _entityRepository,
                _cubeViewPool,
                cubeRepositoryController
            );

            StructureCreationService structureCreationService = new StructureCreationService
            (
                _signalBus,
                _idGenerator,
                cubeCreationService,
                structureReader,
                structureViewFactory,
                cubeViewRepository
            );

            StructureService structureService = new StructureService(structureCreationService);

            CreateStructureSignalAction createStructureSignalAction = new CreateStructureSignalAction
                (structureService, structureCreationService);

            ActivateCubeSignalAction activateCubeSignalAction = new ActivateCubeSignalAction(structureService);

            SetCubeColorSignalAction setCubeColorSignalAction = new SetCubeColorSignalAction(_entityRepository);
            DisposeSignalAction disposeSignalAction = new DisposeSignalAction(_entityRepository);

            return new StructureSignalController
            (
                new ISignalAction[]
                {
                    createStructureSignalAction,
                    activateCubeSignalAction,
                    setCubeColorSignalAction,
                    disposeSignalAction,
                }
            );
        }
    }
}
