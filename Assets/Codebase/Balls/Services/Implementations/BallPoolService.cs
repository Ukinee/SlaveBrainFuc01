using System;
using Codebase.Balls.Models;
using Codebase.Balls.Presentations.Implementations;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using Codebase.Gameplay.Shooting.Services.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class BallPoolService
    {
        private readonly BallViewPool _ballViewPool;
        private readonly ICollisionService _collisionService;
        private readonly IBallMover _ballMover;

        public BallPoolService
        (
            BallViewPool ballViewPool,
            ICollisionService collisionService,
            IBallMover ballMover
        )
        {
            _ballViewPool = ballViewPool;
            _collisionService = collisionService;
            _ballMover = ballMover;
        }

        public BallModel Get(Vector3 position, Vector3 direction)
        {
            BallView view = _ballViewPool.Get();
            BallModel ballModel = new BallModel();
            BallPresenter ballPresenter = new BallPresenter(_collisionService, _ballMover, ballModel, view);

            ballModel.SetPosition(position.WithY(UnityConstants.YOffset));
            ballModel.SetDirection(direction);
            view.Construct(ballPresenter);
            ballPresenter.Enable();

            return ballModel;
        }
    }
}
