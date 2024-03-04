using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Cubes.Models;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Structures.Common;
using Codebase.Structures.Infrastructure.Services.Interfaces;
using Codebase.Structures.Models;
using Codebase.Structures.Presentations.Implementations;
using Codebase.Structures.Views.Implementations;
using UnityEngine;
using static Codebase.Core.Common.Application.Utils.Constants.StructuresConstants;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureCreationService
    {
        private readonly ICubePoolService _cubePoolService;
        private readonly IStructureReader _structureReader;
        private FragmentationService _fragmentationService;
        private StructureViewFactory _structureViewFactory;

        public StructureCreationService
        (
            ICubePoolService cubePoolService,
            IStructureReader structureReader,
            FragmentationService fragmentationService,
            StructureViewFactory structureViewFactory
        )
        {
            _cubePoolService = cubePoolService;
            _structureReader = structureReader;
            _fragmentationService = fragmentationService;
            _structureViewFactory = structureViewFactory;
        }

        public void CreateStructure(string structureName, Vector3 position)
        {
            StructureDto structureDto = _structureReader.Read(structureName);

            int height = structureDto.Cubes.GetLength(0);
            int width = structureDto.Cubes.GetLength(1);

            (var structureView, var amountView) = _structureViewFactory.CreateViews();

            StructureModel structureModel = new StructureModel();

            StructureService structureService = new StructureService(_fragmentationService, structureModel, width, height);

            FillStructure
            (
                structureDto,
                structureService,
                structureView,
                width,
                height
            );

            StructurePresenter structurePresenter = new StructurePresenter(structureModel, structureView, amountView);

            structureView.transform.SetPositionAndRotation(position, Quaternion.identity);

            structurePresenter.Enable();
        }

        private void FillStructure
        (
            StructureDto structureDto,
            StructureService structureService,
            StructureView structureView,
            int width,
            int height
        )
        {
            float startX = width * BlockSize / 2 - BlockSize / 2;
            float startY = height * BlockSize / 2 - BlockSize / 2;

            Vector3 firstCubePosition = new Vector3(-startX, 0, startY);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                CubeColor cubeColor = structureDto.Cubes[y, x].Color;
                Vector3 localPosition = firstCubePosition + new Vector3(x * BlockSize, 0, -y * BlockSize);

                CubeModel cubeModel = _cubePoolService.Create(cubeColor, localPosition, structureView);
                structureService.Add(cubeModel, y, x);
            }
        }
    }
}
