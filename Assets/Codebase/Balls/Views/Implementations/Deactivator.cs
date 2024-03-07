using System;
using Codebase.Cubes.Views.Implementations;
using Codebase.Tank.Services.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Views.Implementations
{
    public class Deactivator : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallCollisionHandler handler))
            {
                handler.OnDeactivatorCollision();
            }

            if (other.TryGetComponent(out CubeView cubeView))
            {
                cubeView.OnDeactivatorCollision();
            }
        }
    }
}
