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
        private IBallPresenter _presenter;

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void SetPool(IPool<BallView> pool) =>
            _pool = pool;

        public void ReturnToPool()
        {
            _presenter = null;
            _ballCollisionHandler.ResetPresenter();
            _pool.Release(this);
        }

        public void Construct(IBallPresenter presenter)
        {
            _presenter = presenter;
            _ballCollisionHandler.Construct(presenter);
        }

        public void Release()
        {
            _presenter.Dispose();
        }
    }
}
