using Codebase.Core.Services.NewInputSystem.Interfaces;
using UnityEngine.EventSystems;

namespace Codebase.Core.Services.NewInputSystem.General
{
    public class IsCursorOverUiProvider : IIsCursorOverUiProvider
    {
        public bool IsCursorOverUi { get; private set; }

        public void OnUpdate()
        {
            IsCursorOverUi = EventSystem.current.IsPointerOverGameObject();
        }
    }
}
