﻿using Codebase.Tank.Services.Interfaces;
using UnityEngine;

namespace Codebase.Tanks.Services.Implementations
{
    public class TankPositionCalculator : ITankPositionCalculator
    {
        public Vector3 CalculatePosition(float position)
        {
            return Vector3.one * 4;
        }

        public float CalculatePosition(Vector3 position)
        {
            return 0;
        }
    }
}
