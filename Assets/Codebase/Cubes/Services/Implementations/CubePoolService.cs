using Codebase.Core.Common.Application.Types;
using Codebase.Cubes.Models;
using Codebase.Cubes.Presentations.Implementations;
using Codebase.Cubes.Services.Interfaces;
using Codebase.Cubes.Views.Implementations;
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

        public void Create(CubeColor color, Vector3 localPosition, Transform parent)
        {
            CubeModel cubeModel = new CubeModel();
            CubeView cubeView = _cubePool.Get(localPosition, parent);
            CubePresenter cubePresenter = new CubePresenter(cubeModel, cubeView);
            cubeView.Construct(cubePresenter);
            
            cubeModel.SetColor(color);
            cubeView.Deactivate();
            cubePresenter.Enable();
        }
    }
}
