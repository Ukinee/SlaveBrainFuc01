using System;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.PoolTags;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Services.Pools;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class BallViewPool :  PoolBase<BallView>
    {
        private readonly BallPoolTag _gameObject;

        public BallViewPool(Func<BallView> factory) : base(factory)
        {
            _gameObject = UnityEngine.Object.FindObjectOfType<BallPoolTag>()
                          ?? new GameObject(nameof(BallPoolService)).AddComponent<BallPoolTag>();
            UnityEngine.Object.DontDestroyOnLoad(_gameObject);
        }

        public BallView Get()
        {
            BallView view = GetInternal();
            
            view.gameObject.SetActive(true);
            
            return view;
        }

        public override void ReleaseAll()
        {
            for (int i = WanderingObjects.Count - 1; i >= 0; i--)
            {
                WanderingObjects[i].Release();
            }
        }

        protected override void OnBeforeReturn(BallView obj)
        {
            $"{obj.gameObject.name} was returned to pool".Log();
            obj.gameObject.SetActive(false);
        }

        protected override void OnCreate(BallView obj)
        {
            obj.SetPool(this);
            obj.transform.SetParent(_gameObject.transform);
        }
    }
}
