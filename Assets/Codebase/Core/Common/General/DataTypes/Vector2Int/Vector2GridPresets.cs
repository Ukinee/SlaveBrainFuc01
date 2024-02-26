namespace ApplicationCode.Core.Common.General.DataTypes.Vector2Int
{
    public partial struct Vector2Grid
    {
        public static Vector2Grid Invalid => new Vector2Grid(666, 666);
        
        public static Vector2Grid Zero => new Vector2Grid(0, 0);
        public static Vector2Grid One => new Vector2Grid(1, 1);
        
        public static Vector2Grid Forward => new Vector2Grid(1, 0);
        public static Vector2Grid Backward => new Vector2Grid(-1, 0);
        public static Vector2Grid Left => new Vector2Grid(0, -1);
        public static Vector2Grid Right => new Vector2Grid(0, 1);
    }
}