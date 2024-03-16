using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using Codebase.Core.Common.Application.Types;
using Codebase.Cubes.CQRS.Queries;
using Codebase.Cubes.Models;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Implementations;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Cubes.Views.Implementations;
using Codebase.Gameplay.Cubes.Controllers.ServiceCommands;
using Codebase.Gameplay.Cubes.Presentations.Implementations;
using Codebase.Structures.CQRS.Commands;
using Codebase.Structures.Services.Interfaces;
using UnityEngine;

namespace Codebase.Gameplay.Cubes.Services.Implementations
{
    public class CubeCreationService : ICubeCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly CubeViewPool _cubeViewPool;
        private readonly IStructureService _structureService;
        private readonly CubeRepositoryController _cubeRepositoryController;
        private readonly GetColorQuery _getColorQuery;
        private readonly CubeDeactivatorCollisionHandler _cubeDeactivatorCollisionHandler;

        public CubeCreationService
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            CubeViewPool cubeViewPool,
            IStructureService structureService,
            CubeRepositoryController cubeRepositoryController,
            CubeDeactivatorCollisionHandler cubeDeactivatorCollisionHandler
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _cubeViewPool = cubeViewPool;
            _structureService = structureService;
            _cubeRepositoryController = cubeRepositoryController;
            _cubeDeactivatorCollisionHandler = cubeDeactivatorCollisionHandler;
            _getColorQuery = new GetColorQuery(_entityRepository);
        }

        public int Create(CubeColor cubeColor, Vector3 globalPosition)
        {
            int id = _idGenerator.Generate();

            CubeModel cubeModel = new CubeModel(id);
            _entityRepository.Register(cubeModel);

            CubeView cubeView = _cubeViewPool.Get();
            _cubeRepositoryController.Register(cubeModel, cubeView);
            
            CubePresenter cubePresenter = new CubePresenter(id, _structureService, _cubeDeactivatorCollisionHandler, _getColorQuery, cubeView);
            
            cubeModel.SetColor(cubeColor);
            
            cubeView.Construct(cubePresenter);
            cubeView.transform.position = globalPosition;
            
            cubePresenter.Enable();
            
            return id;
        }
    }
}
