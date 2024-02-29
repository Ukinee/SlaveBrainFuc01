using System;
using System.Collections.Generic;
using Codebase.Cubes.Models;
using Codebase.Structures.Models;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureController
    {
        private readonly StructureModel _structureModel;
        private readonly List<CubeModel> _cubes = new List<CubeModel>();

        public StructureController(StructureModel structureModel)
        {
            _structureModel = structureModel ?? throw new ArgumentNullException(nameof(structureModel));
        }

        public void AddCube(CubeModel cube)
        {
            _cubes.Add(cube);
            _structureModel.Add();
            
            cube.Destroyed += RemoveCube;
        }

        private void RemoveCube(CubeModel cube)
        {
            if(_cubes.Remove(cube) == false)
                return;
            
            _structureModel.Remove();
            
            cube.Destroyed -= RemoveCube;
        }
    }
}
