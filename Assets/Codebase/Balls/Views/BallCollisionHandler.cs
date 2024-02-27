using UnityEngine;

namespace Codebase.Balls.Views
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BallCollisionHandler : MonoBehaviour
    {
        [SerializeField] private BallView _ball;
        
        private void OnCollisionEnter(Collision other)
        {
            //todo: handle ball collision
            Vector3 newDirection = Vector3.Reflect(_ball.Direction, other.GetContact(0).normal);
            _ball.SetDirection(newDirection);
        }
    }
}
