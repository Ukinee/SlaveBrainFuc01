using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuLeaderboardFormPresenter : IMainMenuLeaderboardFormPresenter
    {
        private readonly IInterfaceService _interfaceService;

        public MainMenuLeaderboardFormPresenter(IInterfaceService interfaceService)
        {
            _interfaceService = interfaceService;
        }
        
        public void OnBackClick()
        {
            _interfaceService.Hide(new MainMenuLeaderboardFormType());
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
