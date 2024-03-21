using Codebase.Balls.Models;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Gameplay.Shooting.CQRS;
using Codebase.Gameplay.Shooting.CQRS.Queries;
using Codebase.Gameplay.Shooting.Services.Interfaces;
using Codebase.Tanks.CQRS;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Gameplay.Shooting.Services.Implementations
{
    public class ShootingService : IShootingService
    {
        private GetTankPositionQuery _tankPositionQuery;
        private BallPoolService _ballPoolService;
        private BallMover _ballMover;

        private ILiveData<int> _ballsToShoot;
        private bool _isDisposed;

        public ShootingService(GetBallsToShootQuery ballsToShootQuery, GetTankPositionQuery tankPositionQuery, BallPoolService ballPoolService, BallMover ballMover)
        {
            _tankPositionQuery = tankPositionQuery;
            _ballPoolService = ballPoolService;
            _ballMover = ballMover;
            
            _ballsToShoot = ballsToShootQuery.Handle();
        }

        public bool IsShooting { get; private set; }

        public async UniTask Shoot(Vector3 targetPosition)
        {
            Vector3 tankPosition = _tankPositionQuery.Handle();
            Vector3 direction = targetPosition - tankPosition;

            IsShooting = true;
            await ShootTask(tankPosition, direction);
            IsShooting = false;
        }

        private async UniTask ShootTask(Vector3 tankPosition, Vector3 direction)
        {
            for (int i = 0; i < _ballsToShoot.Value; i++)
            {
                if (_isDisposed)
                    break;

                Shoot(tankPosition, direction);

                await UniTask.Delay(GameplayConstants.Shooting.TimeToShootMilliseconds);
            }
        }

        private void Shoot(Vector3 tankPosition, Vector3 direction)
        {
            BallModel ball = _ballPoolService.Get(tankPosition, direction);
            _ballMover.Add(ball);
        }

        public void Dispose()
        {
            _ballsToShoot = null;
            _tankPositionQuery = null;
            _ballPoolService = null;
            _ballMover = null;

            _isDisposed = true;
        }
    }
}
