#nullable enable
using Codebase.Core.Common.General.DataTypes.Vector2Int;

namespace Codebase.Core.Common.General.DataTypes.GridPosition
{
    public partial struct Vector3Grid
    {
        public static Vector3Grid operator +(Vector3Grid a, Vector3Grid b) =>
            new Vector3Grid(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3Grid operator -(Vector3Grid a, Vector3Grid b) =>
            new Vector3Grid(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3Grid operator *(Vector3Grid a, Vector3Grid b) =>
            new Vector3Grid(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        public static Vector3Grid operator /(Vector3Grid a, Vector3Grid b) =>
            new Vector3Grid(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        
        public static Vector3Grid operator +(Vector3Grid a, Vector2Grid b) =>
            new Vector3Grid(a.X + b.X, a.Y + b.Y, a.Z);        
        
        public static Vector3Grid operator -(Vector3Grid a, Vector2Grid b) =>
            new Vector3Grid(a.X - b.X, a.Y - b.Y, a.Z);

        public static bool operator ==(Vector3Grid a, Vector3Grid b) =>
            a.X == b.X && a.Y == b.Y && a.Z == b.Z;

        public static bool operator !=(Vector3Grid a, Vector3Grid b) =>
            a.X != b.X || a.Y != b.Y || a.Z != b.Z;

        public static Vector3Grid operator %(Vector3Grid a, int b) =>
            new Vector3Grid(a.X % b, a.Y % b, a.Z % b);

        public static Vector3Grid operator *(Vector3Grid a, int b) =>
            new Vector3Grid(a.X * b, a.Y * b, a.Z * b);

        public static Vector3Grid operator /(Vector3Grid a, int b) =>
            new Vector3Grid(a.X / b, a.Y / b, a.Z / b);

        public static Vector3Grid operator %(Vector3Grid a, Vector2Int.Vector2Grid b) =>
            new Vector3Grid(a.X % b.X, a.Y % b.Y, a.Z);

        public static implicit operator Vector3Grid(Vector2Int.Vector2Grid v) =>
            new Vector3Grid(v.X, v.Y, 0);
        
        public static bool operator <=(Vector3Grid a, Vector3Grid b) =>
            a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z;
        
        public static bool operator >=(Vector3Grid a, Vector3Grid b) =>
            a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z;
        
        public static bool operator <(Vector3Grid a, Vector3Grid b) =>
            a.X < b.X && a.Y < b.Y && a.Z < b.Z;
        
        public static bool operator >(Vector3Grid a, Vector3Grid b) =>
            a.X > b.X && a.Y > b.Y && a.Z > b.Z;
        
        public Vector3Grid Add(int x, int y, int z) =>
            new Vector3Grid(X + x, Y + y, Z + z);
    }
}