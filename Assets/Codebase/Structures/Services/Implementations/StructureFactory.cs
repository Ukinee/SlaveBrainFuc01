using ApplicationCode.Core.Infrastructure.IdGenerators;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using Codebase.Cubes.Services.Implementations;
using Codebase.Cubes.Views.Implementations;
using Codebase.Structures.Models;
using Codebase.Structures.Presentations.Implementations;
using Codebase.Structures.Views.Implementations;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureFactory
    {
        private IIdGenerator _idGenerator;
        private CubeViewRepository _cubeViewRepository;
        private StructureViewFactory _structureViewFactory;

        public StructureFactory(IIdGenerator idGenerator, CubeViewRepository cubeViewRepository, StructureViewFactory structureViewFactory)
        {
            _idGenerator = idGenerator;
            _cubeViewRepository = cubeViewRepository;
            _structureViewFactory = structureViewFactory;
        }

        public StructureModel Create(int[,] cubes)
        {
            int height = cubes.GetLength(0);
            int width = cubes.GetLength(1);

            int id = _idGenerator.Generate();

            StructureModel structureModel = new StructureModel(id, width, height);
            StructureView structureView = _structureViewFactory.Create();
            StructureController structureController = new StructureController(structureModel, structureView);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                int cubeId = cubes[y, x];

                if (cubeId == 0)
                    continue;

                CubeView cubeView = _cubeViewRepository.Get(cubeId);

                cubeView.transform.SetParent(structureView.transform);
                cubeView.transform.localPosition = cubeView.transform.localPosition.WithY(0);
                cubeView.Init(structureView);

                structureModel.Set(cubeId, y, x);
            }

            structureController.Enable();

            return structureModel;
        }
    }
}
