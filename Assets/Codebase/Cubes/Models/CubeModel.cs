using System;
using Codebase.Core.Common.Application.Types;

namespace Codebase.Cubes.Models
{
    public class CubeModel
    {
        public CubeColor Color { get; private set; }
        public bool IsActivated { get; private set; } = false;

        public event Action<CubeColor> ColorChanged;
        public event Action<CubeModel> Activated;

        public CubeModel()
        {
        }

        public void SetColor(CubeColor color)
        {
            Color = color;
            ColorChanged?.Invoke(Color);
        }

        public void Activate()
        {
            IsActivated = true;
            Activated?.Invoke(this);
        }
    }
}
