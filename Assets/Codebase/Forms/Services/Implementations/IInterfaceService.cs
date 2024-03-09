using Codebase.Forms.Common.FormTypes;

namespace Codebase.Forms.Services.Implementations
{
    public interface IInterfaceService
    {
        public void Register(IFormType formType, int id);
        public void Show(IFormType formType);
        public void Hide(IFormType formType);
    }
}
