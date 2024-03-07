using System;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Services.PauseServices;
using Codebase.Tank.Services.Interfaces;
using Codebase.Tanks.CQRS;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Tanks.Services.Implementations
{
    public class TankPositionService : ITankPositionService
    {
        private readonly PauseService _pauseService;
        private readonly ITankPositionCalculator _tankPositionCalculator;
        private readonly SetTankPositionCommand _setTankPositionCommand;
        private readonly GetTankPositionQuery _getTankPositionQuery;

        private bool _isOnCooldown;

        public TankPositionService
        (
            PauseService pauseService,
            ITankPositionCalculator tankPositionCalculator,
            SetTankPositionCommand setTankPositionCommand,
            GetTankPositionQuery getTankPositionQuery
        )
        {
            _pauseService = pauseService;
            _tankPositionCalculator = tankPositionCalculator;
            _setTankPositionCommand = setTankPositionCommand;
            _getTankPositionQuery = getTankPositionQuery;
        }

        public async void SetPosition(Vector3 position)
        {
            if (_isOnCooldown)
                return;

            HandleCooldown();

            float targetPosition = _tankPositionCalculator.CalculatePosition(position);
            await SetPositionRaw(targetPosition);
        }
        
        private async UniTask SetPositionRaw(float targetPosition)
        {
            float tankPosition = _getTankPositionQuery.ExecuteRaw();
            
            while (Math.Abs(tankPosition - targetPosition) > 0.01f)
            {
                tankPosition = Mathf.Lerp(tankPosition, targetPosition, 0.1f); //todo : hardcoded value
                _setTankPositionCommand.Handle(tankPosition);

                await UniTask.WaitWhile(() => _pauseService.IsPaused);
                await UniTask.Yield();
            }
            
            "Position set".Log();
        }

        private async void HandleCooldown()
        {
            _isOnCooldown = true;
            await UniTask.Delay(GameConstants.MillisecondsToTankPositionChange);
            _isOnCooldown = false;
        }
    }
}
