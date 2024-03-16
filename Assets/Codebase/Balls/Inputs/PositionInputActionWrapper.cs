using ApplicationCode.Core.Services.NewInputSystem.Common;
using Codebase.Core.Services.NewInputSystem.Infrastructure;
using Codebase.Core.Services.RaycastHitProviders;
using Codebase.Gameplay.Shooting.Services.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Inputs
{
    public class PositionInputActionWrapper : InputContextActionBase<Vector2>
    {
        private readonly RaycastHitProvider _raycastHitProvider;
        private readonly IAimService _aimService;

        public PositionInputActionWrapper(RaycastHitProvider raycastHitProvider, IAimService aimService)
            : base(InputConstants.Gameplay.InputPosition)
        {
            _raycastHitProvider = raycastHitProvider;
            _aimService = aimService;
        }

        protected override void OnActionStart(Vector2 payload)
        {
            _raycastHitProvider.OnUpdate(payload);
            
            if (_raycastHitProvider.HasHit)
                _aimService.SetPosition(_raycastHitProvider.HitPoint);
        }

        protected override void OnActionUpdate(Vector2 payload)
        {
            _raycastHitProvider.OnUpdate(payload);

            if (_raycastHitProvider.HasHit)
                _aimService.SetPosition(_raycastHitProvider.HitPoint);
            else
                _aimService.EndAim(_raycastHitProvider.HitPoint);
        }

        protected override void OnActionEnd(Vector2 payload)
        {
            _raycastHitProvider.OnUpdate(payload);
            
            if (_raycastHitProvider.HasHit)
                _aimService.SetPosition(_raycastHitProvider.HitPoint);
        }

        public override void Dispose()
        {
        }
    }
}
