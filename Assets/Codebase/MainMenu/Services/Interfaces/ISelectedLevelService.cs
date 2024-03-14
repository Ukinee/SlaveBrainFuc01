namespace Codebase.MainMenu.Services.Interfaces
{
    public interface ISelectedLevelService
    {
        public int CurrentId { get; }
        
        public void Select(int id);
    }
}
