using System;
using UnityEngine;

namespace Codebase.Structures.Views.Interfaces
{
    public interface IStructureView : IDisposable
    {
        public void Collide(Vector3 direction, Vector3 position);
    }
}
