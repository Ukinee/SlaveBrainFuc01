namespace Codebase.Game.Services
{
    public class GameService
    {
        private readonly GameStarter _gameStarter;
        private readonly GameEnder _gameEnder;

        public GameService(GameStarter gameStarter, GameEnder gameEnder)
        {
            _gameStarter = gameStarter;
            _gameEnder = gameEnder;
        }

        public void Start()
        {
            _gameStarter.Start();
        }

        public void End()
        {
            _gameEnder.End();
        }
    }
}
