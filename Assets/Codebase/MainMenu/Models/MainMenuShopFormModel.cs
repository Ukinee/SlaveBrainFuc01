using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Forms.Models;

namespace Codebase.MainMenu.Models
{
    public class MainMenuShopFormModel : FormBase
    {
        private LiveData<int[]> _levels = new LiveData<int[]>(Array.Empty<int>());

        public MainMenuShopFormModel(bool isVisible, int id) : base(isVisible, id)
        {
        }

        public ILiveData<IReadOnlyList<int>> Levels => _levels;

        public void AddLevel(int levelId)
        {
            _levels.Value = _levels.Value.Append(levelId).ToArray();
        }

        protected override void OnDispose()
        {
            _levels.Dispose();

            _levels = null;
        }
    }
}
