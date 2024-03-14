using UnityEngine;

namespace Codebase.Tank.Services.Interfaces
{
    public interface ITankPositionCalculator
    {
        public Vector3 CalculatePosition(float position);
        public float CalculatePosition(Vector3 position);
    }
}
