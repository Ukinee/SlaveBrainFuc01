using Codebase.Core.Frameworks.EnitySystem.General;
using UnityEngine;

namespace Codebase.Balls.Models
{
    public class BallModel
    {
        public Vector3 Position { get; private set; }

        public Vector3 Direction { get; private set; }

        public event System.Action<Vector3> OnPositionChanged;

        public void SetPosition(Vector3 position)
        {
            Position = position;
            OnPositionChanged?.Invoke(Position);
        }

        public void SetDirection(Vector3 direction)
        {
            if(direction == Vector3.zero)
                throw new System.ArgumentException("Zero direction passed to SetDirection.", nameof(direction));
            
            Direction = direction.normalized;
        }
    }
}
