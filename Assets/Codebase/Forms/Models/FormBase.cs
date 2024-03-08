using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.Forms.Models
{
    public abstract class FormBase : BaseEntity
    {
        private readonly LiveData<bool> _visibilityLiveData;

        protected FormBase(bool isVisible, int id) : base(id)
        {
            _visibilityLiveData = new LiveData<bool>(isVisible);
        }

        public ILiveData<bool> VisibilityLiveData => _visibilityLiveData;

        public void SetVisibility(bool isVisible)
        {
            _visibilityLiveData.Value = isVisible;
        }
    }
}
