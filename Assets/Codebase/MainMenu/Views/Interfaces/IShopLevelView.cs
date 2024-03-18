namespace Codebase.MainMenu.Views.Interfaces
{
    public interface IShopLevelView
    {
        public void SetAvailability(bool isAvailable);
        public void SetPrice(int price);
        public void SetId(string gamePresetId);
    }
}
