using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.MainMenu.Models
{
    public class LevelModel : BaseEntity
    {
        private LiveData<bool> _isPassed = new LiveData<bool>(false);
        private LiveData<bool> _isSelected = new LiveData<bool>(false);

        public LevelModel(int id, string levelId) : base(id)
        {
            LevelId = levelId;
        }
        
        public string LevelId { get; }
        public ILiveData<bool> IsPassed => _isPassed;
        public ILiveData<bool> IsSelected => _isSelected;

        public void SetPassed(bool value)
        {
            _isPassed.Value = value;
        }
        
        public void SetSelection(bool value)
        {
            _isSelected.Value = value;
        }

        protected override void OnDispose()
        {
            _isPassed.Dispose();
            _isSelected.Dispose();
        }
    }
}
