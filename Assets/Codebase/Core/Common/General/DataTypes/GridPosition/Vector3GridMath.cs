using System;

namespace Codebase.Core.Common.General.DataTypes.GridPosition
{
    public partial struct Vector3Grid
    {
        public Vector3Grid DeltaY(int delta) =>
            new Vector3Grid(X, Y + delta, Z);

        public Vector3Grid DeltaX(int delta) =>
            new Vector3Grid(X + delta, Y, Z);

        public Vector3Grid DeltaZ(int delta) =>
            new Vector3Grid(X, Y, Z + delta);

        public Vector3Grid WithY(int y) =>
            new Vector3Grid(X, y, Z);

        public Vector3Grid WithX(int x) =>
            new Vector3Grid(x, Y, Z);

        public Vector3Grid WithZ(int z) =>
            new Vector3Grid(X, Y, z);

        public Vector3Grid Sign() =>
            new Vector3Grid(MathF.Sign(X), MathF.Sign(Y), MathF.Sign(Z));

        public void Absolute()
        {
            X = (int)MathF.Abs(X);
            Y = (int)MathF.Abs(Y);
            Z = (int)MathF.Abs(Z);
        }

        public Vector3Grid Abs()
        {
            return new Vector3Grid
            (
                (int)MathF.Abs(X),
                (int)MathF.Abs(Y),
                (int)MathF.Abs(Z)
            );
        }

        public static Vector3Grid Min(Vector3Grid a, Vector3Grid b)
        {
            return new Vector3Grid(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
        }

        public static Vector3Grid Max(Vector3Grid a, Vector3Grid b)
        {
            return new Vector3Grid(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
        }

        public static Vector3Grid Least(Vector3Grid a, Vector3Grid b)
        {
            return a < b ? a : b;
        }

        public static Vector3Grid Biggest(Vector3Grid a, Vector3Grid b)
        {
            return a > b ? a : b;
        }

        public static Vector3Grid RotateAroundZ(Vector3Grid point, Side forward)
        {
            double angle = forward switch
            {
                Side.ZeroPi => 0,
                Side.HalfPi => Math.PI / 2,
                Side.Pi => Math.PI,
                Side.PiAndHalf => Math.PI * 3 / 2,
                _ => throw new ArgumentOutOfRangeException(nameof(forward), forward, null),
            };

            int newX = (int)Math.Round(point.X * Math.Cos(angle) - point.Y * Math.Sin(angle));
            int newY = (int)Math.Round(point.X * Math.Sin(angle) + point.Y * Math.Cos(angle));

            return new Vector3Grid { X = newX, Y = newY, Z = point.Z };
        }
        
        public Vector3Grid WithZ(Vector3Grid other)
        {
            return new Vector3Grid(X, Y, other.Z);
        }
    }
}