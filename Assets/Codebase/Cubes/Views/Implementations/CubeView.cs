using System;
using System.Collections.Generic;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Core.Services.Pools;
using Codebase.Cubes.Presentations.Interfaces;
using Codebase.Cubes.Views.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Codebase.Cubes.Views.Implementations
{
    public class CubeView : ViewBase<ICubePresenter>, ICubeView
    {
        [Serializable]
        class CubeViewData : SerializableDictionary<CubeColor, GameObject>
        {
        }

        [SerializeField] private CubeViewData _cubeViewData;

        private Dictionary<CubeColor, GameObject> _colors;
        private IPool<CubeView> _pool;
        private Rigidbody _rigidBody;
        private bool _isActive;

        private void Awake()
        {
            _colors = _cubeViewData.Dictionary;
        }

        public void Activate()
        {
            if(_isActive)
                return;
            
            _isActive = true;
            transform.SetParent(null);
            _rigidBody = gameObject.AddComponent<Rigidbody>();
            _rigidBody.velocity = Vector3.one * 5;
        }

        public void Deactivate()
        {
            if(_isActive == false)
                return;
            
            _isActive = false;
            Destroy(_rigidBody);
        }

        public void SetColor(CubeColor color)
        {
            foreach (CubeColor cubeColor in _colors.Keys)
                _colors[cubeColor].SetActive(false);

            _colors[color].SetActive(true);
            
            if(color == CubeColor.Transparent)
                gameObject.SetActive(false);
        }

        public void ReturnToPool()
        {
            ResetPresenter();
            _pool.Release(this);
        }

        public void OnBallCollision() =>
            Presenter.Activate();

        public void OnDeactivatorCollision() =>
            Presenter.Dispose();

        public void SetPool(IPool<CubeView> pool) =>
            _pool = pool;

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);
    }
}
