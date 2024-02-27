using System.Collections.Generic;
using Codebase.Balls.Presentations.Interfaces;
using Codebase.Core.Services.Common;

namespace Codebase.Balls.Services.Implementations
{
    public class BallMover : IUpdatable
    {
        private readonly List<IBallPresenter> _balls = new List<IBallPresenter>();

        public void Add(IBallPresenter ballView)
        {
            _balls.Add(ballView);
        }

        public void Remove(IBallPresenter ballView)
        {
            _balls.Remove(ballView);
        }

        public void Update(float deltaTime)
        {
            for (int i = _balls.Count - 1; i >= 0; i--)
            {
                IBallPresenter ball = _balls[i];

                ball.Move(deltaTime);
            }
        }
    }
}
