using System;
using Codebase.Core.Common.Application.Types;

namespace Codebase.Cubes.Models
{
    public class CubeModel
    {
        public CubeColor Color { get; private set; }

        public event Action<CubeColor> ColorChanged;
        public event Action<CubeModel> Destroyed;

        public void SetColor(CubeColor color)
        {
            Color = color;
            ColorChanged?.Invoke(Color);
        }

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
