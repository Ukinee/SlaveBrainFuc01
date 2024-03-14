using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Services.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class WinFormPresenter : IWinFormPresenter
    {
        private readonly IWinFormView _winFormView;
        private readonly IWinFormService _winFormService;

        public WinFormPresenter(IWinFormView winFormView, IWinFormService winFormService)
        {
            _winFormView = winFormView;
            _winFormService = winFormService;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnContinueClick()
        {
            _winFormService.OnContinueClick();
        }
    }
}
