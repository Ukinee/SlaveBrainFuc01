using System;
using Codebase.Balls.Presentations.Implementations;
using Codebase.Balls.Presentations.Interfaces;
using Codebase.Core.Services.Pools;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class BallPool : PoolBase<BallPresenter>
    {
        public BallPool(Func<BallPresenter> factory) : base(factory)
        {
        }

        public IBallPresenter Get(Vector3 position, Vector3 direction)
        {
            BallPresenter presenter = GetInternal();
            
            presenter.Enable();
            presenter.SetPosition(position);
            presenter.SetDirection(direction);

            return presenter;
        }

        protected override void OnCreate(BallPresenter obj)
        {
            obj.SetPool(this);
        }

        protected override void OnBeforeReturn(BallPresenter obj)
        {
            obj.Disable();
        }
    }
}
