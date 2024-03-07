using Codebase.Tank.Services.Interfaces;
using Codebase.Tanks.Model;
using UnityEngine;

namespace Codebase.Tanks.CQRS
{
    public class GetTankPositionQuery
    {
        private readonly TankModel _tank;
        private readonly ITankPositionCalculator _tankPositionCalculator;

        public GetTankPositionQuery(TankModel tank, ITankPositionCalculator tankPositionCalculator)
        {
            _tank = tank;
            _tankPositionCalculator = tankPositionCalculator;
        }

        public Vector3 Execute()
        {
            return _tankPositionCalculator.CalculatePosition(_tank.Position);
        }

        public float ExecuteRaw()
        {
            return _tank.Position;
        }
    }
}
