using Codebase.Core.Common.Application.Types;
using UnityEngine;

namespace Codebase.Cubes.Services.Interfaces
{
    public interface ICubeCreationService
    {
        public int Create( CubeColor cubeColor, Vector3 globalPosition);

    }
}
