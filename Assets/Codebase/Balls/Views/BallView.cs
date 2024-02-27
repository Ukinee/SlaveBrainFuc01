using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Services.Pools;
using UnityEngine;

namespace Codebase.Balls.Views
{
    [RequireComponent(typeof(SphereCollider))]
    public class BallView : MonoBehaviour
    {
        private Transform _transform;
        private IPool<BallView> _pool;
        
        public Vector3 Direction { get; private set; }
        
        private void Awake()
        {
            _transform = transform;
            GetComponent<SphereCollider>().radius = BallConstants.Radius;
        }

        public void SetPool(IPool<BallView> ballPool)
        {
            _pool = ballPool ?? throw new System.ArgumentNullException(nameof(ballPool));
        }
        
        public void Release()
        {
            _pool.Release(this);
        }

        public void SetPosition(Vector3 position)
        {
            if (position == Vector3.zero)
                MaloyAlert.Warning("Zero position passed to SetPosition");

            transform.position = position;
        }

        public void SetDirection(Vector3 direction)
        {
            if (direction == Vector3.zero)
                MaloyAlert.Warning("Zero direction passed to SetDirection");

            Direction = direction.normalized;
        }

        public void Move(Vector3 translation)
        {
            _transform.Translate(translation);
        }
    }
}
