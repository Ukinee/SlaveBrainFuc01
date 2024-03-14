using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
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
            if (_isActive)
                return;

            _isActive = true;
            transform.SetParent(null);
            transform.position = transform.position.WithY(GameConstants.YOffset);
            _rigidBody = gameObject.AddComponent<Rigidbody>();
            _rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
            _collider.excludeLayers += _activatedLayers;
        }

        public void Deactivate()
        {
            if (_isActive == false)
                return;

            _collider.excludeLayers -= _activatedLayers;
            _isActive = false;
            Destroy(_rigidBody);
        }
    }
}
