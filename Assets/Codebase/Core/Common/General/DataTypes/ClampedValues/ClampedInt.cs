using System;

namespace Codebase.Core.Common.General.DataTypes.ClampedValues
{
    public class ClampedInt
    {
        private int _value;

        public ClampedInt(int minValue = default, int maxValue = default)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int Value
        {
            get => _value; 
            set => _value = Math.Clamp(value, MinValue, MaxValue);
        }
        
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        
        public override string ToString() => Value.ToString();
        
        public static int operator +(ClampedInt a, int b) => a.Value + b;
        public static int operator -(ClampedInt a, int b) => a.Value - b;
        public static int operator *(ClampedInt a, int b) => a.Value * b;
        public static int operator /(ClampedInt a, int b) => a.Value / b;
        
        public static int operator +(int a, ClampedInt  b) => a + b.Value;
        public static int operator -(int a, ClampedInt  b) => a - b.Value;
        public static int operator *(int a, ClampedInt  b) => a * b.Value;
        public static int operator /(int a, ClampedInt  b) => a / b.Value;
        
        public static implicit operator int(ClampedInt a) => a.Value;
        public static implicit operator ClampedInt(int a) => new ClampedInt { Value = a, MinValue = 0, MaxValue = int.MaxValue};
        
    }
    
    
}
