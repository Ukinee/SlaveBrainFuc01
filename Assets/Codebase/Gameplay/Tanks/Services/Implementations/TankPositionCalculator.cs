using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Tank.Services.Interfaces;
using UnityEngine;

namespace Codebase.Tanks.Services.Implementations
{
    public class TankPositionCalculator : ITankPositionCalculator
    {
        private readonly Vector3 _mostLeftPosition;
        private readonly Vector3 _mostRightPosition;

        public TankPositionCalculator
        (
            float leftPosition,
            float rightPosition,
            float verticalPosition
        )
        {
            _mostLeftPosition = new Vector3(leftPosition, GameConstants.YOffset, verticalPosition);
            _mostRightPosition = new Vector3(rightPosition, GameConstants.YOffset, verticalPosition);
        }

        public Vector3 CalculatePosition(float position)
        {
            return Vector3.Lerp(_mostLeftPosition, _mostRightPosition, position);
        }

        public float CalculatePosition(Vector3 position)
        {
            return (position.x - _mostLeftPosition.x) / (_mostRightPosition.x - _mostLeftPosition.x);
        }
    }
}
