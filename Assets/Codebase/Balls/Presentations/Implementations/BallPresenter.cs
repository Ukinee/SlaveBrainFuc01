using Codebase.Balls.Models;
using Codebase.Balls.Presentations.Interfaces;
using Codebase.Balls.Services.Interfaces;
using Codebase.Balls.Views.Interfaces;
using Codebase.Core.Services.Pools;
using UnityEngine;

namespace Codebase.Balls.Presentations.Implementations
{
    public class BallPresenter : IBallPresenter
    {
        private readonly ICollisionService _collisionService;
        private readonly IMoveService _moveService;
        private readonly BallModel _ballModel;
        private readonly IBallView _ballView;
        
        private IPool<BallPresenter> _ballPool;

        public BallPresenter
        (
            ICollisionService collisionService,
            IMoveService moveService,
            BallModel ballModel,
            IBallView ballView
        )
        {
            _collisionService = collisionService;
            _moveService = moveService;
            _ballModel = ballModel;
            _ballView = ballView;
        }

        public void Enable()
        {
            _ballModel.OnPositionChanged += OnPositionChanged;
            _ballView.Enable();

            OnPositionChanged(_ballModel.Position);
        }

        public void Disable()
        {
            _ballModel.OnPositionChanged -= OnPositionChanged;
            _ballView.Disable();
        }

        public void SetPosition(Vector3 position) =>
            _ballModel.SetPosition(position);

        public void SetDirection(Vector3 direction) =>
            _ballModel.SetDirection(direction);

        public void Move(float deltaTime) =>
            _moveService.Move(_ballModel, deltaTime);

        public void Collide(Vector3 normal) =>
            _collisionService.Collide(_ballModel, normal);

        public void ReturnToPool() =>
            _ballPool.Release(this);
        
        public void SetPool(IPool<BallPresenter> ballPool) =>
            _ballPool = ballPool;

        private void OnPositionChanged(Vector3 position) =>
            _ballView.SetPosition(position);
    }
}
