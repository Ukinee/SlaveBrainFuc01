using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Models
{
    public class MainMenuMapModel : BaseEntity
    {
        private LiveData<bool> _selectionLiveData;
        private LiveData<bool> _boughtLiveData;

        public MainMenuMapModel(int id, MapType mapType, int price, bool isSelected, bool isBought) : base(id)
        {
            MapType = mapType;
            Price = price;
            _selectionLiveData = new LiveData<bool>(isSelected);
            _boughtLiveData = new LiveData<bool>(isBought);
        }

        public ILiveData<bool> IsSelected => _selectionLiveData;
        public ILiveData<bool> IsBought => _boughtLiveData;
        public MapType MapType { get; }
        public int Price { get; }

        public void SetSelection(bool value) =>
            _selectionLiveData.Value = value;
        
        public void SetBought(bool value) =>
            _boughtLiveData.Value = value;
    }
}
