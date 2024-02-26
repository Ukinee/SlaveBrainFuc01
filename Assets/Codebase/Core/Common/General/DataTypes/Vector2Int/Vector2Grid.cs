using System;

namespace Codebase.Core.Common.General.DataTypes.Vector2Int
{
    [Serializable]
    public partial struct Vector2Grid
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Grid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() =>
            $"(x: {X}, y: {Y})";

        public bool Equals(Vector2Grid other) =>
            X == other.X && Y == other.Y;

        public override bool Equals(object obj) =>
            obj is Vector2Grid other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(X, Y);
    }
}