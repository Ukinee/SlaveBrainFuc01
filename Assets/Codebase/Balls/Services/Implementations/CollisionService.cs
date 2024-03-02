using Codebase.Balls.Models;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class CollisionService : ICollisionService
    {
        public void Collide(BallModel ball, Vector3 normal)
        {
            Vector3 newDirection = Vector3.Reflect(ball.Direction, normal);
            newDirection = Vector3.ProjectOnPlane(newDirection, Vector3.up);
            
            ball.SetDirection(newDirection);
        }
    }
}
