using System;
using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.Cubes.Presentations.Interfaces
{
    public interface ICubePresenter : IPresenter, IDisposable
    {
        public void Activate();
        public void Deactivate();
    }
}
