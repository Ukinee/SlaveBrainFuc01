using Codebase.Balls.Models;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Tanks.CQRS;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class ShootingService : IShootingService
    {
        private readonly GetTankPositionQuery _tankPositionQuery;
        private readonly BallPoolService _ballPoolService;
        private readonly BallMover _ballMover;

        private int _ballsToShoot = BallConstants.DefaultAmountToShoot;
        
        public ShootingService(GetTankPositionQuery tankPositionQuery, BallPoolService ballPoolService, BallMover ballMover)
        {
            _tankPositionQuery = tankPositionQuery;
            _ballPoolService = ballPoolService;
            _ballMover = ballMover;
        }
        
        public void Shoot(Vector3 targetPosition)
        {
            Vector3 tankPosition = _tankPositionQuery.Execute();
            Vector3 direction = targetPosition - tankPosition;

            for (int i = 0; i < _ballsToShoot; i++)
            {
                BallModel ball = _ballPoolService.Get(tankPosition, direction);
                
                _ballMover.Add(ball);
            }
        }
    }
}
