namespace ApplicationCode.Core.Common.General.DataTypes.GridPosition
{
    /// <summary>
    /// Assuming that Z is height
    /// </summary>
    public partial struct Vector3Grid
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3Grid(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3Grid(int x, int y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public override string ToString()
        {
            return $"x: {X} y: {Y} z: {Z}";
        }
        
        public bool Equals(Vector3Grid other) =>
            X == other.X && Y == other.Y && Z == other.Z;

        public override bool Equals(object? obj) =>
            obj is Vector3Grid other && Equals(other);

        public override int GetHashCode()
        {
            int hashCode1 = Y.GetHashCode();
            int hashCode2 = Z.GetHashCode();
            return X.GetHashCode() ^ (hashCode1 << 4) ^ (hashCode1 >> 28) ^ (hashCode2 >> 4) ^ (hashCode2 << 28);
        }
    }
}