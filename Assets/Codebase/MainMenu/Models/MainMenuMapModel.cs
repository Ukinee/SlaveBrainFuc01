using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Models
{
    public class MainMenuMapModel : BaseEntity
    {
        private LiveData<bool> _selectionLiveData;

        public MainMenuMapModel(int id, MapType mapType, bool isSelected) : base(id)
        {
            MapType = mapType;
            _selectionLiveData = new LiveData<bool>(isSelected);
        }

        public ILiveData<bool> IsSelected => _selectionLiveData;
        public MapType MapType { get; }

        public void SetSelection(bool value) =>
            _selectionLiveData.Value = value;
    }
}
