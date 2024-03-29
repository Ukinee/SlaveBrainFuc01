﻿using System.Collections.Generic;
using Codebase.Balls.Models;
using Codebase.Balls.Presentations.Interfaces;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Services.Common;

namespace Codebase.Balls.Services.Implementations
{
    public class BallMover : IUpdatable, IBallMover
    {
        private readonly IMoveService _moveService;
        private readonly List<BallModel> _balls = new List<BallModel>();

        public BallMover(IMoveService moveService)
        {
            _moveService = moveService;
        }

        public void Add(BallModel ballView)
        {
            _balls.Add(ballView);
        }

        public void Remove(BallModel ballView)
        {
            _balls.Remove(ballView);
        }

        public void Update(float deltaTime)
        {
            for (int i = _balls.Count - 1; i >= 0; i--)
            {
                BallModel ball = _balls[i];

                _moveService.Move(ball, deltaTime);
            }
        }
    }
}
