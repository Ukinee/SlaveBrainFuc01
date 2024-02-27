using Codebase.Balls.Presentations.Interfaces;
using Codebase.Balls.Views.Interfaces;
using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Core.Common.General.Utils;
using UnityEngine;

namespace Codebase.Balls.Views.Implementations
{
    [RequireComponent(typeof(SphereCollider))]
    public class BallView : MonoBehaviour, IBallView
    {
        [SerializeField] private BallCollisionHandler _ballCollisionHandler;

        private void Awake()
        {
            GetComponent<SphereCollider>().radius = BallConstants.Radius;
        }

        public void SetPosition(Vector3 position)
        {
            if (position == Vector3.zero)
                MaloyAlert.Warning("Zero position passed to SetPosition");

            transform.position = position;
        }

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        public void Construct(IBallPresenter presenter) =>
            _ballCollisionHandler.Construct(presenter);
    }
}
