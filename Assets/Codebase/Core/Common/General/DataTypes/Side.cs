using System;

namespace Codebase.Core.Common.General.DataTypes
{
    public enum Side : byte
    {
        Right = 0,
        ZeroPi = 0,
        
        Up = 1,
        HalfPi = 1,
        
        Left = 2,
        Pi = 2,
        
        Down = 3,
        PiAndHalf = 3,
    }

    public static class SideExtensions
    {
        public static Side Opposite(this Side side) => 
            side switch
            {
                Side.Up => Side.Down,
                Side.Down => Side.Up,
                Side.Left => Side.Right,
                Side.Right => Side.Left,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
    }
}