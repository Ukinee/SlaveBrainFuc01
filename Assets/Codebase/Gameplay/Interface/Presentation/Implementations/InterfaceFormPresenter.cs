using Codebase.Core.Services.PauseServices;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class InterfaceFormPresenter : IInterfaceFormPresenter
    {
        private readonly PauseService _pauseService;
        private readonly IInterfaceService _interfaceService;
        private readonly IInterfaceFormView _view;

        public InterfaceFormPresenter(PauseService pauseService, IInterfaceService interfaceService, IInterfaceFormView view)
        {
            _pauseService = pauseService;
            _interfaceService = interfaceService;
            _view = view;
        }

        public void Enable()
        {
            string[] options = { };
        }

        public void Disable()
        {
        }

        public void OnPauseButtonPressed()
        {
            _interfaceService.Show(new GameplayPauseFormType());
            _pauseService.PauseGame();
        }
    }
}
