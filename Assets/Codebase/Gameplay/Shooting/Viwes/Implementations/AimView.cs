using Codebase.Gameplay.Shooting.Viwes.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Views.Implementations
{
    public class AimView : MonoBehaviour, IAimView
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        private readonly Vector3[] _positions = new Vector3[2];

        public void SetPoints(Vector3 startPoint, Vector3 endPoint)
        {
            _positions[0] = startPoint;
            _positions[1] = endPoint;
            
            _lineRenderer.SetPositions(_positions);
        }

        public void Enable()
        {
            _lineRenderer.enabled = true;
        }

        public void Disable()
        {
            _lineRenderer.enabled = false;
        }
    }
}
