using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Forms.Models;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Models
{
    public class MainMenuLevelSelectionFormModel : FormBase
    {
        private LiveData<int[]> _levels = new LiveData<int[]>(Array.Empty<int>());
        private LiveData<MapType[]> _maps = new LiveData<MapType[]>(Array.Empty<MapType>());

        public MainMenuLevelSelectionFormModel(bool isVisible, int id) : base(isVisible, id)
        {
        }
        
        public ILiveData<IReadOnlyList<int>> Levels => _levels;
        public ILiveData<IReadOnlyList<MapType>> Maps => _maps;

        public void AddLevel(int levelId)
        {
            _levels.Value = _levels.Value.Append(levelId).ToArray();
        }
        
        public void AddMap(MapType mapType)
        {
            _maps.Value = _maps.Value.Append(mapType).ToArray();
        }

        protected override void OnDispose()
        {
            _levels.Dispose();
            _maps.Dispose();

            _maps = null;
            _levels = null;
        }
    }
}
