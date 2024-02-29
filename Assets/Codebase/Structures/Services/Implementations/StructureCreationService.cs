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

        public void CreateStructure(string structureName)
        {
            StructureDto structureDto = _structureReader.Read(structureName);

            StructureView structureView = _assetProvider.Instantiate<StructureView>(_structureViewPath);
            AmountView amountView = _assetProvider.Instantiate<AmountView>(_amountViewPath);
            //_interfaceService.AddAmountView(amountView);

            StructureModel structureModel = new StructureModel();
            StructurePresenter structurePresenter = new StructurePresenter(structureModel, structureView, amountView);
            StructureController structureController = new StructureController(structureModel);

            FillStructure(structureDto, structureView, structureController);

            structurePresenter.Enable();
        }

        private void FillStructure(StructureDto structureDto, Component structureView, StructureController structureController)
        {
            
            int width = structureDto.Cubes.GetLength(1);
            int height = structureDto.Cubes.GetLength(0);

            float startX = width * BlockSize / 2 - BlockSize / 2;
            float startY = height * BlockSize / 2 - BlockSize / 2;

            Vector3 firstCubePosition = new Vector3(-startX, startY);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                CubeColor cubeColor = structureDto.Cubes[y, x].Color;
                Vector3 localPosition = firstCubePosition + new Vector3(x * BlockSize, -y * BlockSize);

                CubeModel cubeModel = _cubePoolService.Create(cubeColor, localPosition, structureView.transform);
                
                structureController.AddCube(cubeModel);
            }
        }
    }
}
