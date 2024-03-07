using Codebase.Balls.Models;
using Codebase.Balls.Presentations.Interfaces;
using Codebase.Balls.Services.Interfaces;
using Codebase.Balls.Views.Interfaces;
using Codebase.Core.Services.Pools;
using Codebase.Tank.Services.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Presentations.Implementations
{
    public class BallPresenter : IBallPresenter
    {
        private ITankPositionService _tankPositionService;
        private readonly ICollisionService _collisionService;
        private readonly IBallMover _ballMover;
        private BallModel _ballModel;
        private IBallView _ballView;

        public BallPresenter
        (
            ICollisionService collisionService,
            IBallMover ballMover,
            BallModel ballModel,
            IBallView ballView,
            ITankPositionService tankPositionService
        )
        {
            _collisionService = collisionService;
            _ballMover = ballMover;
            _ballModel = ballModel;
            _ballView = ballView;
            _tankPositionService = tankPositionService;
        }

        public Vector3 Direction => _ballModel.Direction;

        public void Enable()
        {
            _ballModel.OnPositionChanged += OnPositionChanged;
        }

        public void Disable()
        {
            _ballModel.OnPositionChanged -= OnPositionChanged;
        }

        public void Collide(Vector3 normal) =>
            _collisionService.Collide(_ballModel, normal);

        public void OnDeactivatorCollision(Vector3 position)
        {
            _tankPositionService.SetPosition(position);
            Dispose();
        }

        private void OnPositionChanged(Vector3 position) =>
            _ballView.SetPosition(position);

        private void Dispose()
        {
            Disable();
            _ballMover.Remove(_ballModel);
            _ballView.ReturnToPool();
            _ballView = null;
            _ballModel = null;
        }
    }
}
