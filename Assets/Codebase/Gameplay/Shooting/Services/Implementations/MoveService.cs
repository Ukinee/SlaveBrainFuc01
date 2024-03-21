using Codebase.Balls.Models;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Gameplay.Shooting.Services.Interfaces;
using UnityEngine;

namespace Codebase.Gameplay.Shooting.Services.Implementations
{
    public class MoveService : IMoveService
    {
        public void Move(BallModel ballModel, float deltaTime)
        {
            Vector3 deltaPosition = ballModel.Direction * (GameplayConstants.Balls.Speed * deltaTime);
            
            ballModel.SetPosition(ballModel.Position + deltaPosition);
        }
    }
}
