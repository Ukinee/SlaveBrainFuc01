using Codebase.Balls.Models;

namespace Codebase.Balls.Services.Interfaces
{
    public interface IMoveService
    {
        public void Move(BallModel ball, float deltaTime);
    }
}
