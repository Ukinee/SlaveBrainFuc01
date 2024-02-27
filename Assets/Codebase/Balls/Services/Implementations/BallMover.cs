using System.Collections.Generic;
using Codebase.Balls.Views;
using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Core.Services.Common;

namespace Codebase.Balls.Services.Implementations
{
    public class BallMover : IUpdatable
    {
        private readonly List<BallView> _balls = new List<BallView>();
        
        public void Add(BallView ballView)
        {
            _balls.Add(ballView);
        }
        
        public void Remove(BallView ballView)
        {
            _balls.Remove(ballView);
        }
        
        public void Update(float deltaTime)
        {
            for (int i = _balls.Count - 1; i >= 0; i--)
            {
                BallView ball = _balls[i];
                
                ball.Move(ball.Direction * (BallConstants.Speed * deltaTime));
            }
        }
    }
}
