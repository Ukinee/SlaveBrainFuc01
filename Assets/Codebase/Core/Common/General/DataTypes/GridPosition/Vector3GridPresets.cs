namespace ApplicationCode.Core.Common.General.DataTypes.GridPosition
{
    public partial struct Vector3Grid
    {
        public static Vector3Grid Zero => new Vector3Grid(0, 0, 0);
        public static Vector3Grid One => new Vector3Grid(1, 1, 1);
        
        public static Vector3Grid Right => new Vector3Grid(1, 0, 0);
        public static Vector3Grid Forward => new Vector3Grid(0, 1, 0);
        public static Vector3Grid Up => new Vector3Grid(0, 0, 1);
        
        public static Vector3Grid Left => new Vector3Grid(-1, 0, 0);
        public static Vector3Grid Backward => new Vector3Grid(0, -1, 0);
        public static Vector3Grid Down => new Vector3Grid(0, 0, -1);
        
        public static Vector3Grid Invalid => new Vector3Grid(666, 666, 666);
    }
}