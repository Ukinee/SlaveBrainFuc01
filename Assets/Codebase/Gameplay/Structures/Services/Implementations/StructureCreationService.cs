using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Structures.Common;
using Codebase.Structures.Infrastructure.Services.Interfaces;
using Codebase.Structures.Models;
using UnityEngine;

namespace Codebase.Gameplay.Structures.Services.Implementations
{
    public class StructureCreationService
    {
        private readonly ICubeCreationService _cubeCreationService;
        private readonly IStructureReader _structureReader;
        private readonly StructureFactory _structureFactory;
        
        private const float BlockSize = GameplayConstants.Structures.BlockSize;

        public StructureCreationService
        (
            ICubeCreationService cubeCreationService,
            IStructureReader structureReader,
            StructureFactory structureFactory
        )
        {
            _cubeCreationService = cubeCreationService;
            _structureReader = structureReader;
            _structureFactory = structureFactory;
        }

        public StructureModel CreateStructure(string structureName, Vector3 position)
        {
            StructureDto structureDto = _structureReader.Read(structureName);

            int[,] cubes = CreateCubes(structureDto, position);

            return _structureFactory.Create(cubes);
        }

        private int[,] CreateCubes(StructureDto structureDto, Vector3 position)
        {
            int height = structureDto.Cubes.GetLength(0);
            int width = structureDto.Cubes.GetLength(1);

            int[,] cubes = new int[height, width];

            float startX = width * BlockSize / 2 - BlockSize / 2;
            float startY = height * BlockSize / 2 - BlockSize / 2;

            Vector3 firstCubePosition = new Vector3(-startX, 0, startY) + position;

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                CubeColor cubeColor = structureDto.Cubes[y, x].Color;

                if (cubeColor == CubeColor.Transparent)
                    continue;

                Vector3 globalPosition = firstCubePosition + new Vector3(x * BlockSize, 0, -y * BlockSize);

                int cubeModel = _cubeCreationService.Create(cubeColor, globalPosition);
                cubes[y, x] = cubeModel;
            }

            return cubes;
        }
    }
}
