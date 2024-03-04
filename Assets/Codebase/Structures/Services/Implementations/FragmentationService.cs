using System;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using Codebase.Core.Infrastructure.Repositories.Base;
using Codebase.Cubes.Models;
using Codebase.Cubes.Views.Implementations;
using Codebase.Structures.Models;
using Codebase.Structures.Presentations.Implementations;
using Codebase.Structures.Views.Implementations;

namespace Codebase.Structures.Services.Implementations
{
    public class FragmentationService
    {
        private readonly StructureViewFactory _structureViewFactory;
        private readonly IReadonlyDictionaryRepository<CubeModel, CubeView> _cubeRepository;

        public FragmentationService
        (
            StructureViewFactory structureViewFactory,
            IReadonlyDictionaryRepository<CubeModel, CubeView> cubeRepository
        )
        {
            _structureViewFactory = structureViewFactory;
            _cubeRepository = cubeRepository;
        }

        public void Handle(CubeModel[][,] islands)
        {
            $"Handling fragmentation of {islands.Length} islands".Log();
            Random random = new Random();
            
            foreach (CubeModel[,] island in islands)
            {
                CubeColor color = ((CubeColor[])Enum.GetValues(typeof(CubeColor)))[random.Next(0, 17)];
                HandleIsland(island, color);
            }
        }

        private void HandleIsland(CubeModel[,] island, CubeColor color)
        {
            int width = island.GetLength(1);
            int height = island.GetLength(0);

            StructureModel structureModel = new StructureModel();
            (StructureView structureView, AmountView amountView) = _structureViewFactory.CreateViews();
            StructurePresenter structurePresenter = new StructurePresenter(structureModel, structureView, amountView);
            StructureService structureService = new StructureService(this, structureModel, width, height);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                CubeModel cubeModel = island[y, x];

                if (cubeModel == null)
                    continue;
                
                cubeModel.SetColor(color);

                CubeView cubeView = _cubeRepository.Get(cubeModel);

                cubeView.Init(structureView);
                cubeView.transform.SetParent(structureView.transform);
                cubeView.transform.localPosition = cubeView.transform.localPosition.WithY(0);
                structureService.Add(cubeModel, y, x);
            }

            structurePresenter.Enable();
        }
    }
}
