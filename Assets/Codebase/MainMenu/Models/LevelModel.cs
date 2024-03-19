using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.MainMenu.Models
{
    public class LevelModel : BaseEntity
    {
        private LiveData<bool> _isPassed;
        private LiveData<bool> _isSelected;
        private LiveData<bool> _isUnlocked;

        public LevelModel(int id, string levelId, int price, bool isUnlocked, bool isSelected, bool isPassed) : base(id)
        {
            LevelId = levelId;
            Price = price;
            
            _isUnlocked = new LiveData<bool>(isUnlocked);
            _isSelected = new LiveData<bool>(isSelected);
            _isPassed = new LiveData<bool>(isPassed);
        }
        
        public string LevelId { get; }
        public int Price { get; }
        public ILiveData<bool> IsPassed => _isPassed;
        public ILiveData<bool> IsSelected => _isSelected;
        public ILiveData<bool> IsUnlocked => _isUnlocked;

        public void SetPassed(bool value)
        {
            _isPassed.Value = value;
        }
        
        public void SetSelection(bool value)
        {
            _isSelected.Value = value;
        }
        
        public void SetUnlocked(bool value)
        {
            _isUnlocked.Value = value;
        }

        protected override void OnDispose()
        {
            _isPassed.Dispose();
            _isSelected.Dispose();
        }
    }
}
