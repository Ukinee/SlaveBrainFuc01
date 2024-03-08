using Codebase.Balls.Services.Implementations;
using Codebase.Cubes.Services.Implementations;

namespace Codebase.Game.Services
{
    public class GameEnder
    {
        private readonly BallViewPool _ballViewPool;
        private readonly CubeViewPool _cubeViewPool;

        public GameEnder(BallViewPool ballViewPool, CubeViewPool cubeViewPool)
        {
            _ballViewPool = ballViewPool;
            _cubeViewPool = cubeViewPool;
        }

        public void End()
        {
            _ballViewPool.ReleaseAll();
            _cubeViewPool.ReleaseAll();
        }
    }
}
