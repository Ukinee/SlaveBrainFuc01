using Codebase.Core.Common.Application.Types;
using Codebase.Cubes.Models;
using UnityEngine;

namespace Codebase.Cubes.Services.Interfaces
{
    public interface ICubePoolService
    {
        public CubeModel Create(CubeColor color, Vector3 localPosition, Transform parent);
    }
}
