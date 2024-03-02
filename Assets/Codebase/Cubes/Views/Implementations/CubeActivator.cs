using UnityEngine;

namespace Codebase.Cubes.Views.Implementations
{
    public class CubeActivator : MonoBehaviour
    {
        [SerializeField] CubeView _cubeView;
        [SerializeField] private Collider _collider;
        [SerializeField] private LayerMask _activatedLayers;
        
        private bool _isActive;
        private Rigidbody _rigidBody;
        
        public void Activate()
        {
            if(_isActive)
                return;
            
            _isActive = true;
            transform.SetParent(null);
            _rigidBody = gameObject.AddComponent<Rigidbody>();
            _collider.excludeLayers += _activatedLayers;
        }

        public void Deactivate()
        {
            if(_isActive == false)
                return;
            
            _collider.excludeLayers -= _activatedLayers;
            _isActive = false;
            Destroy(_rigidBody);
        }
    }
}
