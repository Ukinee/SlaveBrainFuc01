namespace Codebase.MainMenu.Views.Interfaces
{
    public interface ILevelView : ILevelSelectorPartView
    {
        public void SetSelected(bool isSelected);
        public void SetPassed(bool isPassed);
        public void SetLevelName(string levelId);
    }
}
