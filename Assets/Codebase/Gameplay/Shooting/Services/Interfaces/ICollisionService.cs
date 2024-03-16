using Codebase.Balls.Models;
using UnityEngine;

namespace Codebase.Gameplay.Shooting.Services.Interfaces
{
    public interface ICollisionService
    {
        public void Collide(BallModel ball, Vector3 normal);
    }
}
