using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using Assets.Codebase.Core.Frameworks.EnitySystem.Repositories;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using Codebase.Core.Common.Application.Types;
using Codebase.Cubes.CQRS.Queries;
using Codebase.Cubes.Models;
using Codebase.Cubes.Presentations.Implementations;
using Codebase.Cubes.Repositories.Implementations;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Cubes.Views.Implementations;
using Codebase.Structures.Views.Implementations;
using UnityEngine;

namespace Codebase.Cubes.Services.Implementations
{
    public class CubeCreationService : ICubeCreationService
    {
        private readonly ISignalBus _signalBus;
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly CubeViewPool _cubeViewPool;
        private readonly CubeRepositoryController _cubeRepositoryController;
        private readonly GetColorQuery _getColorQuery;

        public CubeCreationService
        (
            ISignalBus signalBus,
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            CubeViewPool cubeViewPool,
            CubeRepositoryController cubeRepositoryController
        )
        {
            _signalBus = signalBus;
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _cubeViewPool = cubeViewPool;
            _cubeRepositoryController = cubeRepositoryController;
            _getColorQuery = new GetColorQuery(_entityRepository);
        }

        public int Create(CubeColor cubeColor, Vector3 globalPosition)
        {
            int id = _idGenerator.Generate();

            CubeModel cubeModel = new CubeModel(id);
            _entityRepository.Register(cubeModel);

            CubeView cubeView = _cubeViewPool.Get();
            _cubeRepositoryController.Register(cubeModel, cubeView);
            
            CubePresenter cubePresenter = new CubePresenter(id, _signalBus, _getColorQuery, cubeView);
            
            cubeModel.SetColor(cubeColor);
            
            cubeView.Construct(cubePresenter);
            cubeView.transform.position = globalPosition;
            
            cubePresenter.Enable();
            
            return id;
        }
    }
}
