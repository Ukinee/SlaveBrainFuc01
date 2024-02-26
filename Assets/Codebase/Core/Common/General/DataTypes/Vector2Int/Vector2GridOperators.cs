#nullable enable
using System;
using Codebase.Core.Common.General.DataTypes.GridPosition;

namespace Codebase.Core.Common.General.DataTypes.Vector2Int
{
    public partial struct Vector2Grid
    {
        public static Vector2Grid operator +(Vector2Grid a, Vector2Grid b) => new Vector2Grid(a.X + b.X, a.Y + b.Y);

        public static Vector2Grid operator -(Vector2Grid a, Vector2Grid b) => new Vector2Grid(a.X - b.X, a.Y - b.Y);

        public static Vector2Grid operator *(Vector2Grid a, Vector2Grid b) => new Vector2Grid(a.X * b.X, a.Y * b.Y);

        public static Vector2Grid operator /(Vector2Grid a, Vector2Grid b) => new Vector2Grid(a.X / b.X, a.Y / b.Y);

        public static Vector2Grid operator %(Vector2Grid a, Vector2Grid b) => new Vector2Grid(a.X % b.X, a.Y % b.Y);

        public static bool operator ==(Vector2Grid a, Vector2Grid b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Vector2Grid a, Vector2Grid b) => a.X != b.X || a.Y != b.Y;

        public static Vector2Grid operator +(Vector2Grid a, int b) => new Vector2Grid(a.X + b, a.Y + b);

        public static Vector2Grid operator -(Vector2Grid a, int b) => new Vector2Grid(a.X - b, a.Y - b);

        public static Vector2Grid operator *(Vector2Grid a, int b) => new Vector2Grid(a.X * b, a.Y * b);

        public static Vector2Grid operator /(Vector2Grid a, int b) => new Vector2Grid(a.X / b, a.Y / b);

        public static Vector2Grid operator %(Vector2Grid a, int b) => new Vector2Grid(a.X % b, a.Y % b);

        public static implicit operator Vector2Grid(Vector3Grid v) => new Vector2Grid { X = v.X, Y = v.Y };

        public Vector2Grid Sign()
        {
            return new Vector2Grid(MathF.Sign(X), MathF.Sign(Y));
        }

        public static Vector2Grid Min(Vector2Grid start, Vector2Grid end)
        {
            return new Vector2Grid(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y)); 
        }
        
        public static Vector2Grid Max(Vector2Grid start, Vector2Grid end)
        {
            return new Vector2Grid(Math.Max(start.X, end.X), Math.Max(start.Y, end.Y));
        }
    }
}