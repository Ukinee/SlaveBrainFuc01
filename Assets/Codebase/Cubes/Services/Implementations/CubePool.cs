using System;
using Codebase.Core.Common.Application.PoolTags;
using Codebase.Core.Services.Pools;
using Codebase.Cubes.Presentations.Implementations;
using Codebase.Cubes.Views.Implementations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Cubes.Services.Implementations
{
    public class CubePool : PoolBase<CubeView>
    {
        private readonly CubePoolTag _gameObject;

        public CubePool(Func<CubeView> factory) : base(factory)
        {
            _gameObject = Object.FindObjectOfType<CubePoolTag>()
                          ?? new GameObject(nameof(CubePool)).AddComponent<CubePoolTag>();

            Object.DontDestroyOnLoad(_gameObject);
        }

        public CubeView Get(Vector3 localPosition, Transform parent)
        {
            CubeView cubeView = GetInternal();

            if (parent == null)
                return cubeView;

            cubeView.transform.SetParent(parent, false);
            cubeView.transform.localPosition = localPosition;
            cubeView.gameObject.SetActive(true);

            return cubeView;
        }

        protected override void OnCreate(CubeView obj)
        {
            obj.SetPool(this);
        }

        protected override void OnBeforeReturn(CubeView obj)
        {
            obj.transform.SetParent(_gameObject.transform);
            obj.gameObject.SetActive(false);
        }
    }
}
