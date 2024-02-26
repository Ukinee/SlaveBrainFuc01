using ApplicationCode.Core.Common.General.DataTypes.GridPosition;
using ApplicationCode.Core.Common.General.DataTypes.Vector2Int;
using Codebase.Core.Common.Application.Utilities.Constants;

namespace Codebase.Core.Common.Application.Utilities
{
    public static class CoordinateCalculator
    {
        /// <summary>
        /// Converts global position to chunk coordinate 
        /// </summary>
        /// <param name="globalCoordinate"> Global position on grid </param>
        /// <returns></returns>
        public static Vector2Grid GetChunkCoordinate(Vector3Grid globalCoordinate)
        {
            Vector2Grid chunkCoordinate = globalCoordinate / MapConsts.ChunkSize;

            if (globalCoordinate.Y < 0 && globalCoordinate.Y % MapConsts.ChunkSize != 0)
                chunkCoordinate.Y -= 1;

            if (globalCoordinate.X < 0 && globalCoordinate.X % MapConsts.ChunkSize != 0)
                chunkCoordinate.X -= 1;

            return chunkCoordinate;
        }

        public static Vector3Grid GetLocalCoordinate(Vector3Grid globalCoordinate)
        {
            Vector3Grid chunkCoordinate = GetChunkCoordinate(globalCoordinate);

            return globalCoordinate - chunkCoordinate * MapConsts.ChunkSize;
        }

        public static Vector3Grid GetLocalCoordinate(Vector3Grid globalCoordinate, Vector2Grid chunkCoordinate)
        {
            Vector3Grid vector3ChunkCoordinate = chunkCoordinate; 
            return globalCoordinate - vector3ChunkCoordinate * MapConsts.ChunkSize;
        }

        public static Vector3Grid GetGlobalCoordinate(Vector2Grid chunkCoordinate)
        {
            return chunkCoordinate * MapConsts.ChunkSize;
        }
    }
}