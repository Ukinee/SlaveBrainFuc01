using Codebase.Core.Common.Application.Types;
using Codebase.Cubes.Models;
using Codebase.Cubes.Presentations.Implementations;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Cubes.Views.Implementations;
using Codebase.Structures.Views.Implementations;
using UnityEngine;

namespace Codebase.Cubes.Services.Implementations
{
    public class CubePoolService : ICubePoolService
    {
        private readonly CubePool _cubePool;

        public CubePoolService(CubePool cubePool)
        {
            _cubePool = cubePool;
        }

        public CubeModel Create(CubeColor color, Vector3 localPosition, StructureView parent)
        {
            CubeView cubeView = _cubePool.Get(localPosition, parent.transform);
            
            CubeModel cubeModel = new CubeModel();
            CubePresenter cubePresenter = new CubePresenter(cubeModel, cubeView);
            cubeView.Construct(cubePresenter);
            cubeView.Initialize(parent);
            cubeModel.SetColor(color);
            cubeView.Deactivate();
            cubePresenter.Enable();

            return cubeModel;
        }
    }
}
