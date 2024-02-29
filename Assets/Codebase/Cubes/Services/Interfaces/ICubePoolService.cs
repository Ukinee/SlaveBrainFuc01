using Codebase.Core.Common.Application.Types;
using UnityEngine;

namespace Codebase.Cubes.Services.Interfaces
{
    public interface ICubePoolService
    {
        public void Create(CubeColor color, Vector3 localPosition, Transform parent);
    }
}
