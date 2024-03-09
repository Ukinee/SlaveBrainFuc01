using Codebase.Forms.Factories;

namespace Codebase.App.Infrastructure.StateMachines.States
{
    public class MainMenuScene : ISceneState
    {
        private readonly MainMenuFactory _mainMenuFactory;

        public MainMenuScene(MainMenuFactory mainMenuFactory)
        {
            _mainMenuFactory = mainMenuFactory;
        }
        
        public void Enter()
        {
            _mainMenuFactory.Create();
        }

        public void Exit()
        {
        }
    }
}
