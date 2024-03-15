using Codebase.Gameplay.Interface.Services.Interfaces;

namespace Codebase.Gameplay.Interface.Services.Implementations
{
    public class WinFormService : IWinFormService
    {
        public bool IsContinueClicked { get; private set; }

        public void OnContinueClick() =>
            IsContinueClicked = true;

        public bool GetContinueClicked() =>
            IsContinueClicked;
    }
}
