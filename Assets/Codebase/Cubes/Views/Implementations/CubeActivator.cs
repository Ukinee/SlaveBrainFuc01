using UnityEngine;

namespace Codebase.Cubes.Views.Implementations
{
    public class CubeActivator : MonoBehaviour
    {
        [SerializeField] CubeView _cubeView;

        private bool _isActive;
        private Rigidbody _rigidBody;
        
        public void Activate()
        {
            if(_isActive)
                return;
            
            _isActive = true;
            transform.SetParent(null);
            _rigidBody = gameObject.AddComponent<Rigidbody>();
            _rigidBody.velocity = Vector3.one * 5;
        }

        public void Deactivate()
        {
            if(_isActive == false)
                return;
            
            _isActive = false;
            Destroy(_rigidBody);
        }
    }
}
