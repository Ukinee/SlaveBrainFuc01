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

        public ShootInputAction
        (
            IAimService aimService,
            IShootingService shootService
        )
            : base(InputConstants.Gameplay.Shoot)
        {
            _aimService = aimService;
            _shootService = shootService;
        }

        protected override void OnActionStart(object payload)
        {
            _aimService.StartAim();
        }

        protected override void OnActionUpdate(object payload)
        {
        }

        protected override void OnActionEnd(object payload)
        {
            _aimService.EndAim();
            _shootService.Shoot(_aimService.AimPosition);
        }

        public override void Dispose()
        {
        }
    }
}
