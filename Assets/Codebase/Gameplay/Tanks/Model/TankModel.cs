using System;

namespace Codebase.Tanks.Model
{
    public class TankModel
    {
        private float _position;

        public float Position
        {
            get => _position;
            set
            {
                _position = Math.Clamp(value, 0, 1f); 
                PositionChanged?.Invoke(_position);
            }
        }

        public event Action<float> PositionChanged;
    }
}
