using Codebase.Forms.Views.Interfaces;
using UnityEngine;

namespace Codebase.Forms.Views.Implementations
{
    public class InterfaceView : MonoBehaviour, IInterfaceView
    {
        [SerializeField] private Transform _root;

        public void SetChild(IFormView formView)
        {
            Transform childTransform = ((MonoBehaviour)formView).transform;
            
            childTransform.SetParent(_root, false);
        }
    }
}
