using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Services.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class WinFormPresenter : IWinFormPresenter
    {
        private readonly int _id;
        private readonly IWinFormView _winFormView;
        private readonly IWinFormService _winFormService;
        private readonly DisposeCommand _disposeCommand;

        public WinFormPresenter(int id, IWinFormView winFormView, IWinFormService winFormService, DisposeCommand disposeCommand)
        {
            _id = id;
            _winFormView = winFormView;
            _winFormService = winFormService;
            _disposeCommand = disposeCommand;
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
        
        public void OnViewDisposed()
        {
            Dispose();
        }

        private void Dispose()
        {
            _disposeCommand.Handle(_id);
        }
    }
}
