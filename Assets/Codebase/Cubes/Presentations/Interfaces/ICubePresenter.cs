using System;
using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.Cubes.Presentations.Interfaces
{
    public interface ICubePresenter : IPresenter, IDisposable
    {
        public void OnBallCollision();
        public void OnDeactivatorCollision();
    }
}
