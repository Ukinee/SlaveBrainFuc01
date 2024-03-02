using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
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
        private readonly AssetProvider _assetProvider;
        private readonly string _structureViewPath;
        private readonly string _amountViewPath;

        public StructureCreationService
        (
            ICubePoolService cubePoolService,
            IStructureReader structureReader,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _cubePoolService = cubePoolService;
            _structureReader = structureReader;
            _assetProvider = assetProvider;
            _structureViewPath = filePathProvider.Structures.Data[PathConstants.Structures.Structure];
            _amountViewPath = filePathProvider.Structures.Data[PathConstants.Structures.AmountView];
        }

        public void CreateStructure(string structureName, Vector3 position)
        {
            StructureDto structureDto = _structureReader.Read(structureName);

            StructureView structureView = _assetProvider.Instantiate<StructureView>(_structureViewPath);
            AmountView amountView = _assetProvider.Instantiate<AmountView>(_amountViewPath);
            //_interfaceService.AddAmountView(amountView);

            StructureModel structureModel = new StructureModel();
            StructurePresenter structurePresenter = new StructurePresenter(structureModel, structureView, amountView);
            StructureController structureController = new StructureController(structureModel, structurePresenter);

            FillStructure(structureDto, structureView, structureController);

            structureView.transform.SetPositionAndRotation(position, Quaternion.identity);

            structurePresenter.Enable();
        }

        private void FillStructure
            (StructureDto structureDto, StructureView structureView, StructureController structureController)
        {
            int width = structureDto.Cubes.GetLength(1);
            int height = structureDto.Cubes.GetLength(0);

            float startX = width * BlockSize / 2 - BlockSize / 2;
            float startY = height * BlockSize / 2 - BlockSize / 2;

            Vector3 firstCubePosition = new Vector3(-startX,0, startY);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                CubeColor cubeColor = structureDto.Cubes[y, x].Color;
                Vector3 localPosition = firstCubePosition + new Vector3(x * BlockSize, 0, -y * BlockSize);

                CubeModel cubeModel = _cubePoolService.Create(cubeColor, localPosition, structureView);

                structureController.AddCube(cubeModel);
            }
        }
    }
}
