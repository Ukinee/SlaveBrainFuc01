using System;
using Codebase.Core.Common.Application.PoolTags;
using Codebase.Core.Services.Pools;
using Codebase.Cubes.Views.Implementations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Cubes.Services.Implementations
{
    public class CubeViewPool : PoolBase<CubeView>
    {
        private readonly CubePoolTag _gameObject;

        public CubeViewPool(Func<CubeView> factory) : base(factory)
        {
            _gameObject = Object.FindObjectOfType<CubePoolTag>()
                          ?? new GameObject(nameof(CubeViewPool)).AddComponent<CubePoolTag>();

            Object.DontDestroyOnLoad(_gameObject);
        }

        public CubeView Get()
        {
            CubeView cubeView = GetInternal();

            cubeView.gameObject.SetActive(true);

            return cubeView;
        }

        protected override void OnCreate(CubeView obj)
        {
            obj.SetPool(this);
        }

        protected override void OnBeforeReturn(CubeView obj)
        {
            obj.Deactivate();
            obj.transform.SetParent(_gameObject.transform);
            obj.gameObject.SetActive(false);
        }
    }
}
