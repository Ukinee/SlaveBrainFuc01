using Codebase.Balls.Services.Implementations;
using Codebase.Cubes.Services.Implementations;
using Codebase.Gameplay.Shooting.Services.Interfaces;

namespace Codebase.Game.Services.Implementations
{
    public class GameEnder
    {
        private readonly BallViewPool _ballViewPool;
        private readonly CubeViewPool _cubeViewPool;
        private readonly IShootingService _shootingService;

        public GameEnder
        (
            BallViewPool ballViewPool,
            CubeViewPool cubeViewPool,
            IShootingService shootingService
        )
        {
            _ballViewPool = ballViewPool;
            _cubeViewPool = cubeViewPool;
            _shootingService = shootingService;
        }

        public void End()
        {
            _shootingService.Dispose();
            _ballViewPool.ReleaseAll();
            _cubeViewPool.ReleaseAll();
        }
    }
}
