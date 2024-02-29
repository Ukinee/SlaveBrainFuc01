using Codebase.Core.Common.Application.Types;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Cubes.Presentations.Interfaces;

namespace Codebase.Cubes.Views.Interfaces
{
    public interface ICubeView : IView<ICubePresenter>
    {
        public void SetColor(CubeColor color);

        public void Activate();
        public void Deactivate();
        public void ReturnToPool();
    }
}
