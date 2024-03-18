using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.MainMenu.Models
{
    public class ShopLevelModel : BaseEntity
    {
        public ShopLevelModel(int id, string gamePresetId, int price) : base(id)
        {
            GamePresetId = gamePresetId;
            Price = price;
        }

        public string GamePresetId { get; }
        public int Price { get; }
    }
}
