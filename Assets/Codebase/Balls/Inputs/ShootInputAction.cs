using System;
using ApplicationCode.Core.Services.NewInputSystem.Common;
using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Services.Common;
using Codebase.Core.Services.NewInputSystem.Infrastructure;

namespace Codebase.Balls.Inputs
{
    public class ShootInputAction : InputContextActionBase<object>
    {
        private readonly IAimService _aimService;
        private readonly IShootingService _shootService;
        private readonly IRaycastHitProvider _raycastHitProvider;

        public ShootInputAction
        (
            IAimService aimService,
            IShootingService shootService,
            IRaycastHitProvider raycastHitProvider
        )
            : base(InputConstants.Gameplay.Shoot)
        {
            _aimService = aimService;
            _shootService = shootService;
            _raycastHitProvider = raycastHitProvider;
        }

        protected override void OnActionStart(object payload)
        {
            _aimService.StartAim(_raycastHitProvider.HitPoint);
        }

        protected override void OnActionUpdate(object payload)
        {
        }

        protected override void OnActionEnd(object payload)
        {
            _aimService.EndAim(_raycastHitProvider.HitPoint);
            _shootService.Shoot(_aimService.AimPosition);
        }

        public override void Dispose()
        {
        }
    }
}
