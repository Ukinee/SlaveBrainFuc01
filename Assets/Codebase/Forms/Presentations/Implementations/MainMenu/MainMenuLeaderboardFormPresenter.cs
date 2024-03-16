using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuLeaderboardFormPresenter : IMainMenuLeaderboardFormPresenter
    {
        private readonly int _id;
        private readonly IInterfaceService _interfaceService;
        private readonly DisposeCommand _disposeCommand;

        public MainMenuLeaderboardFormPresenter(int id, IInterfaceService interfaceService, DisposeCommand disposeCommand)
        {
            _id = id;
            _interfaceService = interfaceService;
            _disposeCommand = disposeCommand;
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

        public void OnViewDisposed()
        {
            _disposeCommand.Handle(_id);
        }
    }
}
