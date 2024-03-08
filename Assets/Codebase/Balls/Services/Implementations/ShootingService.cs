using Codebase.Balls.Models;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Tanks.CQRS;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class ShootingService : IShootingService
    {
        private GetTankPositionQuery _tankPositionQuery;
        private BallPoolService _ballPoolService;
        private BallMover _ballMover;

        private int _ballsToShoot = BallConstants.DefaultAmountToShoot;
        private bool _isDisposed;

        public bool IsShooting { get; private set; }

        public ShootingService(GetTankPositionQuery tankPositionQuery, BallPoolService ballPoolService, BallMover ballMover)
        {
            _tankPositionQuery = tankPositionQuery;
            _ballPoolService = ballPoolService;
            _ballMover = ballMover;
        }

        public async UniTask Shoot(Vector3 targetPosition)
        {
            Vector3 tankPosition = _tankPositionQuery.Execute();
            Vector3 direction = targetPosition - tankPosition;

            IsShooting = true;
            await ShootTask(tankPosition, direction);
            IsShooting = false;
        }

        private async UniTask ShootTask(Vector3 tankPosition, Vector3 direction)
        {
            for (int i = 0; i < _ballsToShoot; i++)
            {
                if(_isDisposed)
                    break;
                
                Shoot(tankPosition, direction);

                await UniTask.Delay(GameConstants.MillisecondsToShoot);
            }
        }

        private void Shoot(Vector3 tankPosition, Vector3 direction)
        {
            BallModel ball = _ballPoolService.Get(tankPosition, direction);
            _ballMover.Add(ball);
        }

        public void Dispose()
        {
            _tankPositionQuery = null;
            _ballPoolService = null;
            _ballMover = null;
            
            _isDisposed = true;
        }
    }
}
