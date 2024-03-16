using Codebase.Balls.Models;

namespace Codebase.Gameplay.Shooting.Services.Interfaces
{
    public interface IMoveService
    {
        public void Move(BallModel ball, float deltaTime);
    }
}
