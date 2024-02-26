using System;
using ApplicationCode.Core.Common.General.DataTypes.GridPosition;
using UnityEngine;

namespace Codebase.Core.Common.Application.Extensions
{
    public static class Vector3ToVector3Grid
    {
        public static Vector3Grid ToVector3Int(this Vector3 vector3)
        {
            int x = (int)MathF.Round(vector3.x);
            int y = (int)MathF.Round(vector3.y);
            int z = (int)MathF.Round(vector3.z);

            return new Vector3Grid
            (
                x,
                z,
                y
            ); // Intentional. In Vector3Grid, Z is up
        }

        public static Vector3 FromVector3Int(this Vector3Grid vector3Grid)
        {
            return new Vector3
            (
                vector3Grid.X,
                vector3Grid.Z,
                vector3Grid.Y
            );
        }
    }
}
