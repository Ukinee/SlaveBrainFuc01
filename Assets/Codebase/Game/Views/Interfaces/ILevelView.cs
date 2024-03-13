namespace Codebase.Game.Views.Interfaces
{
    public interface ILevelView
    {
        public void SetSelected(bool isSelected);
        public void SetPassed(bool isPassed);
        public void UnParent();
        public void SetLevelName(string levelId);
    }
}
