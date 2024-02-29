using System;
using Codebase.Core.Common.Application.Types;

namespace Codebase.Structures.Common
{
    [Serializable]
    public class StructureDto
    {
        public CubeDto[,] Cubes;
    }

    [Serializable]
    public class CubeDto
    {
        public CubeColor Color;
    }
}
