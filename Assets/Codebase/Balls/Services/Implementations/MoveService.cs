using Codebase.Balls.Models;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Common.Application.Utilities.Constants;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class MoveService : IMoveService
    {
        public void Move(BallModel ballModel, float deltaTime)
        {
            Vector3 deltaPosition = ballModel.Direction * BallConstants.Speed * deltaTime;
            
            ballModel.SetPosition(ballModel.Position + deltaPosition);
        }
    }
}
