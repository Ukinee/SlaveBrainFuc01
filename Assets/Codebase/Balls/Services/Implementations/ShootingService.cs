using Codebase.Balls.Services.Interfaces;
using Codebase.Balls.Views;
using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Tanks.CQRS;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class ShootingService : IShootingService
    {
        private readonly GetTankPositionQuery _tankPositionQuery;
        private readonly BallPool _ballPool;
        private readonly BallMover _ballMover;

        private int _ballsToShoot = BallConstants.DefaultAmountToShoot;
        
        public ShootingService(GetTankPositionQuery tankPositionQuery, BallPool ballPool, BallMover ballMover)
        {
            _tankPositionQuery = tankPositionQuery;
            _ballPool = ballPool;
            _ballMover = ballMover;
        }
        
        public void Shoot(Vector3 targetPosition)
        {
            Vector3 tankPosition = _tankPositionQuery.Execute();
            Vector3 direction = targetPosition - tankPosition;

            for (int i = 0; i < _ballsToShoot; i++)
            {
                BallView ballView = _ballPool.Get(tankPosition, direction);
                
                _ballMover.Add(ballView);
            }
        }
    }
}
