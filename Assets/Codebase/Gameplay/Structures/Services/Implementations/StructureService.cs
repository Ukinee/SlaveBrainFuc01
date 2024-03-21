using System.Collections.Generic;
using Codebase.Structures.Models;
using Codebase.Structures.Services.Interfaces;

namespace Codebase.Gameplay.Structures.Services.Implementations
{
    public class StructureService : IStructureService
    {
        private List<StructureModel> _structures = new List<StructureModel>();
        private readonly StructureFactory _structureFactory;

        public StructureService(StructureFactory structureFactory)
        {
            _structureFactory = structureFactory;
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
                if (structure.TryRemove(cubeId))
                    break;
            }
        }

        private void OnStructureFragmented(int[][,] islands)
        {
            foreach (int[,] island in islands)
            {
                StructureModel structureModel = _structureFactory.Create(island);

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
