using Codebase.Balls.Presentations.Interfaces;
using Codebase.Cubes.Views.Implementations;
using UnityEngine;

namespace Codebase.Balls.Views.Implementations
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BallCollisionHandler : MonoBehaviour
    {
        private IBallPresenter _ballPresenter;

        public void Construct(IBallPresenter presenter) =>
            _ballPresenter = presenter;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent<Reflector>(out _))
            {
                Vector3 normal = other.GetContact(0).normal;
                _ballPresenter?.Collide(normal);
            }

            if (other.collider.TryGetComponent(out CubeView cubeView))
            {
                cubeView.OnBallCollision(_ballPresenter.Direction, transform.position);
            }
        }
        
        public void OnDeactivatorCollision() =>
            _ballPresenter.OnDeactivatorCollision();

        public void ResetPresenter() =>
            _ballPresenter = null;
    }
}
