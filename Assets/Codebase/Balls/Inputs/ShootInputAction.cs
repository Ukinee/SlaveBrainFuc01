using System;
using ApplicationCode.Core.Services.NewInputSystem.Common;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.NewInputSystem.Infrastructure;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using Codebase.Tank.Services.Interfaces;
using Cysharp.Threading.Tasks;

namespace Codebase.Balls.Inputs
{
    public class ShootInputAction : InputContextActionBase<object>
    {
        private readonly IAimService _aimService;
        private readonly IShootingService _shootService;
        private readonly IRaycastHitProvider _raycastHitProvider;
        private readonly ITankPositionService _tankPositionService;
        private readonly IIsCursorOverUiProvider _isCursorOverUiProvider;

        public ShootInputAction
        (
            IAimService aimService,
            IShootingService shootService,
            IRaycastHitProvider raycastHitProvider,
            ITankPositionService tankPositionService,
            IIsCursorOverUiProvider isCursorOverUiProvider
        )
            : base(InputConstants.Gameplay.Shoot)
        {
            _aimService = aimService;
            _shootService = shootService;
            _raycastHitProvider = raycastHitProvider;
            _tankPositionService = tankPositionService;
            _isCursorOverUiProvider = isCursorOverUiProvider;
        }

        private bool IsBlocked => _tankPositionService.IsMoving || _shootService.IsShooting;

        protected override void OnActionStart(object payload)
        {
            if (IsBlocked || _isCursorOverUiProvider.IsCursorOverUi || _raycastHitProvider.HasHit == false)
                return;
            
            _aimService.StartAim(_raycastHitProvider.HitPoint);
        }

        protected override void OnActionUpdate(object payload)
        {
        }

        protected override async void OnActionEnd(object payload)
        {
            if (IsBlocked || _aimService.IsAiming == false)
                return;
            
            _aimService.EndAim(_raycastHitProvider.HitPoint);
            await _shootService.Shoot(_aimService.AimPosition);
            await _tankPositionService.SetRandomPosition();
        }

        public override void Dispose()
        {
        }
    }
}
