namespace Codebase.Gameplay.Interface.Services.Interfaces
{
    public interface IWinFormService
    {
        public bool IsContinueClicked { get; }

        public void OnContinueClick();
        public bool GetContinueClicked();

        public void Enable(int coinAmount);
    }
}
