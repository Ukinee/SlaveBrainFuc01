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
        
        public void Construct(IBallPresenter presenter)
        {
            _ballPresenter = presenter;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent<Reflector>(out _))
            {
                _ballPresenter.Collide(other.GetContact(0).normal);
            }

            if (other.collider.TryGetComponent(out CubeView cubeView))
            {
                cubeView.OnBallCollision();
            }

            if (other.collider.TryGetComponent<Deactivator>(out _))
            {
                _ballPresenter.ReturnToPool();
            }
        }
    }
}
