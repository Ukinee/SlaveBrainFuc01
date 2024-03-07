using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Cubes.CQRS.Queries;
using Codebase.Cubes.Models;
using Codebase.Cubes.Presentations.Implementations;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Cubes.Views.Implementations;
using Codebase.Structures.CQRS.Commands;
using Codebase.Structures.Services.Implementations;
using Codebase.Structures.Services.Interfaces;
using UnityEngine;

namespace Codebase.Cubes.Services.Implementations
{
    public class CubeCreationService : ICubeCreationService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly CubeViewPool _cubeViewPool;
        private readonly IStructureService _structureService;
        private readonly CubeRepositoryController _cubeRepositoryController;
        private readonly RemoveCubeFromStructureCommand _removeCubeCommand;
        private readonly GetColorQuery _getColorQuery;
        private readonly DisposeCommand _disposeCommand;

        public CubeCreationService
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            CubeViewPool cubeViewPool,
            IStructureService structureService,
            CubeRepositoryController cubeRepositoryController,
            RemoveCubeFromStructureCommand removeCubeCommand
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _cubeViewPool = cubeViewPool;
            _structureService = structureService;
            _cubeRepositoryController = cubeRepositoryController;
            _removeCubeCommand = removeCubeCommand;
            _getColorQuery = new GetColorQuery(_entityRepository);
            _disposeCommand = new DisposeCommand(_entityRepository);
        }

        public int Create(CubeColor cubeColor, Vector3 globalPosition)
        {
            int id = _idGenerator.Generate();

            CubeModel cubeModel = new CubeModel(id);
            _entityRepository.Register(cubeModel);

            CubeView cubeView = _cubeViewPool.Get();
            _cubeRepositoryController.Register(cubeModel, cubeView);
            
            CubePresenter cubePresenter = new CubePresenter(id, _structureService, _disposeCommand, _getColorQuery, cubeView);
            
            cubeModel.SetColor(cubeColor);
            
            cubeView.Construct(cubePresenter);
            cubeView.transform.position = globalPosition;
            
            cubePresenter.Enable();
            
            return id;
        }
    }
}
