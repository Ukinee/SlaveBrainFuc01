#define MALOYDEBUG

using System;
using System.Collections.Generic;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Structures.Models;
using Codebase.Structures.Services.Interfaces;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureService : IStructureService
    {
        private List<StructureModel> _structures = new List<StructureModel>();
        private StructureCreationService _structureCreationService;

        public StructureService(StructureCreationService structureCreationService)
        {
            _structureCreationService = structureCreationService;
        }

        public void Add(StructureModel structureModel)
        {
            _structures.Add(structureModel);

            structureModel.Disposed += RemoveStructure;
            structureModel.Fragmented += OnStructureFragmented;
        }

        public void RemoveCube(int cubeId)
        {
            foreach (StructureModel structure in _structures)
            {
                if (structure.TryGetIndexers(cubeId, out int height, out int width) == false)
                    continue;

                structure.Set(0, height, width);
                structure.HandleFragmentation();

                if (structure.IsEmpty)
                    structure.Dispose();

                break;
            }
        }

        private void OnStructureFragmented(int[][,] islands)
        {
#if MALOYDEBUG
            Random random = new Random();
#endif

            foreach (int[,] island in islands)
            {
#if MALOYDEBUG
                CubeColor randomColor = (CubeColor)random.Next(0, Enum.GetNames(typeof(CubeColor)).Length);
                StructureModel structureModel = _structureCreationService.Create(island, randomColor, true);
#else
                StructureModel structureModel = _structureCreationService.Create(island);
#endif
                Add(structureModel);
            }
        }

        private void RemoveStructure(int structureId)
        {
            StructureModel model = _structures.Find(structure => structure.Id == structureId);
            model.Disposed -= RemoveStructure;
            model.Fragmented -= OnStructureFragmented;

            _structures.Remove(model);
        }
    }
}
