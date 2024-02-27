using System;

namespace Codebase.Tank.Model
{
    public class TankModel
    {
        private float _position;

        public float Position
        {
            get => _position;
            set
            {
                _position = Math.Clamp(value, -1f, 1f); 
                PositionChanged?.Invoke(_position);
            }
        }

        public event Action<float> PositionChanged;
    }
}
