using Codebase.Balls.Presentations.Interfaces;
using Codebase.Balls.Views.Interfaces;
using Codebase.Core.Services.Pools;
using UnityEngine;

namespace Codebase.Balls.Views.Implementations
{
    [RequireComponent(typeof(SphereCollider))]
    public class BallView : MonoBehaviour, IBallView
    {
        [SerializeField] private BallCollisionHandler _ballCollisionHandler;

        private IPool<BallView> _pool;

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void SetPool(IPool<BallView> pool) =>
            _pool = pool;

        public void ReturnToPool()
        {
            _ballCollisionHandler.ResetPresenter();
            _pool.Release(this);
        }

        public void Construct(IBallPresenter presenter) =>
            _ballCollisionHandler.Construct(presenter);
    }
}
