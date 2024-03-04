using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.General;

namespace Codebase.Cubes.Models
{
    public class CubeModel : BaseEntity
    {
        public CubeModel(int id) : base(id)
        {
        }

        private readonly LiveData<CubeColor> _colorData = new LiveData<CubeColor>(CubeColor.Magenta);

        public ILiveData<CubeColor> Color => _colorData;

        public void SetColor(CubeColor color)
        {
            _colorData.Value = color;
        }

        protected override void OnDispose()
        {
            _colorData.Dispose();
        }
    }
}
