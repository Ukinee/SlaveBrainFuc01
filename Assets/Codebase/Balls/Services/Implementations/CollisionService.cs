﻿using Codebase.Balls.Models;
using Codebase.Balls.Services.Interfaces;
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
