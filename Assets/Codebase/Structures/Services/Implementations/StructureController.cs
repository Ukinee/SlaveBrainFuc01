using System.Collections.Generic;
using Codebase.Cubes.Models;
using Codebase.Structures.Models;
using Codebase.Structures.Presentations.Implementations;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureController
    {
        private StructureModel _structureModel;
        private StructurePresenter _structurePresenter;
        private List<CubeModel> _cubes = new List<CubeModel>();

        public StructureController(StructureModel structureModel, StructurePresenter structurePresenter)
        {
            _structureModel = structureModel;
            _structurePresenter = structurePresenter;
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

            if (_cubes.Count == 0)
                Dispose();
        }

        private void Dispose()
        {
            _structurePresenter.Dispose();
            _structureModel.Dispose();
            _cubes = null;
            _structureModel = null;
            _structurePresenter = null;
        }
    }
}
