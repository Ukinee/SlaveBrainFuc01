using Codebase.Core.Common.General.LiveDatas;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Presentations.Interfaces;
using Codebase.Forms.Views.Interfaces;

namespace Codebase.Forms.Presentations.Implementations
{
    public class FormVisibilityPresenter : IFormPresenter
    {
        private readonly IFormView _formView;
        private readonly ILiveData<bool> _formVisibilityLiveData;

        public FormVisibilityPresenter(int formId, GetFormVisibilityQuery getFormVisibilityQuery, IFormView formView)
        {
            _formView = formView;
            _formVisibilityLiveData = getFormVisibilityQuery.Handle(formId);
        }
        
        public void Enable()
        {
            _formVisibilityLiveData.AddListener(OnVisibilityChanged);
        }

        public void Disable()
        {
            _formVisibilityLiveData.RemoveListener(OnVisibilityChanged);
        }

        private void OnVisibilityChanged(bool visibility)
        {
            _formView.SetVisibility(visibility);
        }
    }
}
