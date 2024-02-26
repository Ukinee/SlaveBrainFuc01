using System;
using System.Globalization;

namespace Codebase.Core.Common.General.DataTypes.ClampedValues
{
    public class ClampedFloat
    {
        private float _value;
        
        public ClampedFloat(float minValue = 0, float maxValue = 1)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
        
        public float Value { get => _value; set => _value = Math.Clamp(value, MinValue, MaxValue); }
        
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
        
        public static float operator +(ClampedFloat a, float b) => a.Value + b;
        public static float operator -(ClampedFloat a, float b) => a.Value - b;
        public static float operator *(ClampedFloat a, float b) => a.Value * b;
        public static float operator /(ClampedFloat a, float b) => a.Value / b;
        
        public static float operator +(ClampedFloat a, int b) => a.Value + b;
        public static float operator -(ClampedFloat a, int b) => a.Value - b;
        public static float operator *(ClampedFloat a, int b) => a.Value * b;
        public static float operator /(ClampedFloat a, int b) => a.Value / b;
            
        public static float operator +(float a, ClampedFloat b) => a + b.Value;
        public static float operator -(float a, ClampedFloat b) => a - b.Value;
        public static float operator *(float a, ClampedFloat b) => a * b.Value;
        public static float operator /(float a, ClampedFloat b) => a / b.Value;
        
        public static float operator +(int a, ClampedFloat b) => a + b.Value;
        public static float operator -(int a, ClampedFloat b) => a - b.Value;
        public static float operator *(int a, ClampedFloat b) => a * b.Value;
        public static float operator /(int a, ClampedFloat b) => a / b.Value;
        
        public static implicit operator float(ClampedFloat a) => a.Value;
        public static implicit operator ClampedFloat(float a) => new ClampedFloat() 
            { Value = a };
    }
}
