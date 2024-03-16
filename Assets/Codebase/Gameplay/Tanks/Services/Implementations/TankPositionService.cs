using System;
using Codebase.Core.Services.PauseServices;
using Codebase.Tank.Services.Interfaces;
using Codebase.Tanks.CQRS;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Gameplay.Tanks.Services.Implementations
{
    public class TankPositionService : ITankPositionService
    {
        private readonly PauseService _pauseService;
        private readonly SetTankPositionCommand _setTankPositionCommand;
        private readonly GetTankPositionQuery _getTankPositionQuery;

        public bool IsMoving { get; private set; }

        public TankPositionService
        (
            PauseService pauseService,
            SetTankPositionCommand setTankPositionCommand,
            GetTankPositionQuery getTankPositionQuery
        )
        {
            _pauseService = pauseService;
            _setTankPositionCommand = setTankPositionCommand;
            _getTankPositionQuery = getTankPositionQuery;
        }

        public async UniTask SetRandomPosition()
        {
            IsMoving = true;
            await SetPositionRaw(Random.value);
            IsMoving = false;
        }

        private async UniTask SetPositionRaw(float targetPosition)
        {
            float tankPosition = _getTankPositionQuery.HandleRaw();

            while (Math.Abs(tankPosition - targetPosition) > 0.01f)
            {
                tankPosition = Mathf.Lerp(tankPosition, targetPosition, 5 * Time.deltaTime); //todo : hardcoded value
                _setTankPositionCommand.Handle(tankPosition);

                await UniTask.WaitWhile(_pauseService.GetStatus);
                await UniTask.Yield();
            }
        }
    }
}
