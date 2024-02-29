using Codebase.Structures.Views.Interfaces;
using UnityEngine;

namespace Codebase.Structures.Views.Implementations
{
    public class StructureView : MonoBehaviour, IStructureView
    {
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
