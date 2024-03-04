using ApplicationCode.Core.Infrastructure.IdGenerators;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using Codebase.Cubes.Controllers.Signals;
using Codebase.Cubes.Services.Implementations;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Cubes.Views.Implementations;
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
        private readonly ISignalBus _signalHandler;
        private readonly IIdGenerator _idGenerator;
        private readonly ICubeCreationService _cubeCreationService;
        private readonly IStructureReader _structureReader;
        private readonly CubeViewRepository _cubeViewRepository;
        private StructureViewFactory _structureViewFactory;

        public StructureCreationService
        (
            ISignalBus signalHandler,
            IIdGenerator idGenerator,
            ICubeCreationService cubeCreationService,
            IStructureReader structureReader,
            StructureViewFactory structureViewFactory,
            CubeViewRepository cubeViewRepository
        )
        {
            _signalHandler = signalHandler;
            _idGenerator = idGenerator;
            _cubeCreationService = cubeCreationService;
            _structureReader = structureReader;
            _structureViewFactory = structureViewFactory;
            _cubeViewRepository = cubeViewRepository;
        }

        public StructureModel Create(int[,] cubes, CubeColor cubeColor = default, bool overrideColor = false)
        {
            int height = cubes.GetLength(0);
            int width = cubes.GetLength(1);

            int id = _idGenerator.Generate();

            StructureModel structureModel = new StructureModel(id, width, height);
            StructureView structureView = _structureViewFactory.Create();
            StructureViewController structureViewController = new StructureViewController(structureModel, structureView);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                int cubeId = cubes[y, x];
                
                if (cubeId == 0)
                    continue;

                if (overrideColor)
                    _signalHandler.Handle(new SetCubeColorSignal(cubeId, cubeColor));

                CubeView cubeView = _cubeViewRepository.Get(cubeId);

                cubeView.transform.SetParent(structureView.transform);
                cubeView.transform.localPosition = cubeView.transform.localPosition.WithY(0);
                cubeView.Init(structureView);

                structureModel.Set(cubeId, y, x);
            }

            structureViewController.Enable();

            return structureModel;
        }

        public StructureModel CreateStructure(string structureName, Vector3 position)
        {
            StructureDto structureDto = _structureReader.Read(structureName);

            int[,] cubes = CreateCubes(structureDto, position);

            return Create(cubes);
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
