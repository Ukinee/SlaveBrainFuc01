using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Forms.Models;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Models
{
    public class MainMenuLevelSelectorFormModel : FormBase
    {
        private LiveData<int[]> _levels = new LiveData<int[]>(Array.Empty<int>());
        private LiveData<int[]> _maps = new LiveData<int[]>(Array.Empty<int>());

        public MainMenuLevelSelectorFormModel(bool isVisible, int id) : base(isVisible, id)
        {
        }
        
        public ILiveData<IReadOnlyList<int>> Levels => _levels;
        public ILiveData<IReadOnlyList<int>> Maps => _maps;

        public void AddLevel(int levelId)
        {
            _levels.Value = _levels.Value.Append(levelId).ToArray();
        }
        
        public void AddMap(int mapId)
        {
            _maps.Value = _maps.Value.Append(mapId).ToArray();
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
