using ApplicationCode.Core.Services.RaycastHitProviders;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.NewInputSystem.General.Wrappers.ActionMapWrappers.Base;
using Codebase.Core.Services.NewInputSystem.General.Wrappers.InputActionWrappers;
using Codebase.Core.Services.NewInputSystem.General.Wrappers.InputActionWrappers.Generic;
using Codebase.Core.Services.NewInputSystem.Infrastructure;
using Codebase.Core.Services.NewInputSystem.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Codebase.App.Infrastructure.Builders
{
    public class InputServiceFactory
    {
        private readonly IsCursorOverUiProvider _cursorOverUiProvider;
        private readonly ContextActionService _contextActionService;

        public InputServiceFactory
        (
            IsCursorOverUiProvider cursorOverUiProvider,
            ContextActionService contextActionService
        )
        {
            _cursorOverUiProvider = cursorOverUiProvider;
            _contextActionService = contextActionService;
        }

        public InputService Create()
        {
            GeneralInput generalInput = new GeneralInput();

            InputActionWrapper<Vector2> positionActionWrapper = new InputActionWrapper<Vector2>
            (
                _contextActionService,
                generalInput.Gameplay.InputPosition,
                InputConstants.Gameplay.InputPosition
            );
            
            InputActionWrapper aimAndShootActionWrapper = new InputActionWrapper
            (
                _contextActionService,
                generalInput.Gameplay.Aim,
                InputConstants.Gameplay.Shoot
            );

            InputActionMapWrapper gameplayInputMap = new InputActionMapWrapper
            (
                generalInput.Gameplay,
                new IInputActionWrapper[]
                {
                    positionActionWrapper,
                    aimAndShootActionWrapper,
                }
            );

            return new InputService
            (
                _cursorOverUiProvider,
                generalInput,
                new IInputActionMapWrapper[]
                {
                    gameplayInputMap,
                }
            );
        }
    }
}
