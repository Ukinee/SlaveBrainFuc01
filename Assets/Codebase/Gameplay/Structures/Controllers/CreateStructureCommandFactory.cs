using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Implementations;
using Codebase.Gameplay.Cubes.Controllers.ServiceCommands;
using Codebase.Gameplay.Cubes.Services.Implementations;
using Codebase.Gameplay.Structures.Services.Implementations;
using Codebase.Structures.CQRS.Commands;
using Codebase.Structures.Infrastructure.Services.Implementations;

namespace Codebase.Gameplay.Structures.Controllers
{
    public class CreateStructureCommandFactory
    {
        private AssetProvider _assetProvider;
        private FilePathProvider _filePathProvider;
        private IIdGenerator _idGenerator;
        private IEntityRepository _entityRepository;
        private CubeViewPool _cubeViewPool;
        private CubeRepositoryController _cubeRepositoryController;
        private readonly CubeViewRepository _cubeViewRepository;
        private readonly CubeDeactivatorCollisionHandler _cubeDeactivatorCollisionHandler;

        public CreateStructureCommandFactory
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            CubeViewPool cubeViewPool,
            CubeRepositoryController cubeRepositoryController,
            CubeViewRepository cubeViewRepository,
            CubeDeactivatorCollisionHandler cubeDeactivatorCollisionHandler
        )
        {
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _cubeViewPool = cubeViewPool;
            _cubeRepositoryController = cubeRepositoryController;
            _cubeViewRepository = cubeViewRepository;
            _cubeDeactivatorCollisionHandler = cubeDeactivatorCollisionHandler;
        }

        public CreateStructureCommand Create()
        {

            StructureReader structureReader = new StructureReader(_assetProvider, _filePathProvider);
            StructureViewFactory structureViewFactory = new StructureViewFactory(_assetProvider, _filePathProvider);

            StructureFactory structureFactory = new StructureFactory(_idGenerator, _cubeViewRepository, structureViewFactory);
            StructureService structureService = new StructureService(structureFactory);

            CubeCreationService cubeCreationService = new CubeCreationService
            (
                _idGenerator,
                _entityRepository,
                _cubeViewPool,
                structureService,
                _cubeRepositoryController,
                _cubeDeactivatorCollisionHandler
            );

            StructureCreationService structureCreationService = new StructureCreationService
            (
                cubeCreationService,
                structureReader,
                structureFactory
            );

            return new CreateStructureCommand(structureService, structureCreationService);
        }
    }
}
