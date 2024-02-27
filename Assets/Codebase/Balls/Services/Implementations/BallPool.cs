using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Balls.Views;
using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Services.Pools;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class BallPool : PoolBase<BallView>
    {
        public BallPool(AssetProvider assetProvider, FilePathProvider filePathProvider)
            : base(() => assetProvider.Instantiate<BallView>(filePathProvider.General.Data[PathConstants.General.Ball]))
        {
        }

        public BallView Get(Vector3 position, Vector3 direction)
        {
            BallView ballView = GetInternal();
            ballView.SetPosition(position);
            ballView.SetDirection(direction);

            return ballView;
        }

        protected override void OnCreate(BallView obj)
        {
            obj.SetPool(this);
        }
    }
}
