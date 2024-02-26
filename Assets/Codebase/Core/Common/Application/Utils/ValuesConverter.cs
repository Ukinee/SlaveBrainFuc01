using ApplicationCode.Core.Common.General.DataTypes.GridPosition;
using ApplicationCode.Core.Common.General.DataTypes.Vector2Int;
using Codebase.Core.Common.Application.Utilities;
using Codebase.Core.Common.Application.Utilities.Constants;
using UnityEngine;

namespace Codebase.Core.Common.Application.Utils
{
    public static class ValuesConverter
    {
        public static Vector3Grid GetCellPosition(Vector3 position)
        {
            int y = Mathf.FloorToInt(position.y / UnityConstants.CellHeight);
            int x = Mathf.FloorToInt(position.x / UnityConstants.CellSize);
            int z = Mathf.FloorToInt(position.z / UnityConstants.CellSize);

            return new Vector3Grid(x, z, y);
        }
        
        public static Vector2Grid GetChunkPosition(Vector3 position) =>
            CoordinateCalculator.GetChunkCoordinate(GetCellPosition(position));

        public static Vector3 GetPossibleUnityCellCentre(Vector3 position)
        {
            int y = Mathf.FloorToInt(position.y / UnityConstants.CellHeight);
            int x = Mathf.FloorToInt(position.x / UnityConstants.CellSize);
            int z = Mathf.FloorToInt(position.z / UnityConstants.CellSize);

            return new Vector3(x, y, z) +
                   new Vector3(UnityConstants.CellSize, UnityConstants.CellHeight, UnityConstants.CellSize) / 2;
        }
    }
}