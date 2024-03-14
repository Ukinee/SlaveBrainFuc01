using System;
using Codebase.Core.Frameworks.MVP.Interfaces;
using UnityEngine;

namespace Codebase.Cubes.Presentations.Interfaces
{
    public interface ICubePresenter : IPresenter, IDisposable
    {
        public void OnDeactivatorCollision();
        public void OnBallCollision();
    }
}
