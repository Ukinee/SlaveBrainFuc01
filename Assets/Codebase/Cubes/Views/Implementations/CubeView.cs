﻿using System;
using System.Collections.Generic;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Core.Services.Pools;
using Codebase.Cubes.Presentations.Interfaces;
using Codebase.Cubes.Views.Interfaces;
using Codebase.Structures.Views.Implementations;
using UnityEngine;

namespace Codebase.Cubes.Views.Implementations
{
    public class CubeView : ViewBase<ICubePresenter>, ICubeView
    {
        [Serializable]
        class CubeViewData : SerializableDictionary<CubeColor, GameObject>
        {
        }

        [SerializeField] private CubeActivator _activator;
        [SerializeField] private CubeViewData _cubeViewData;

        private Dictionary<CubeColor, GameObject> _colors;
        private IPool<CubeView> _pool;
        private StructureView _structureView;

        private void Awake() =>
            _colors = _cubeViewData.Dictionary;

        public void Activate() =>
            _activator.Activate();

        public void Deactivate() =>
            _activator.Deactivate();

        public void SetColor(CubeColor color)
        {
            foreach (CubeColor cubeColor in _colors.Keys)
                _colors[cubeColor].SetActive(false);

            _colors[color].SetActive(true);

            if (color == CubeColor.Transparent)
                OnDeactivatorCollision();
        }

        public void OnBallCollision(Vector3 direction, Vector3 position)
        {
            _structureView.Rigidbody.AddForceAtPosition
                (-direction * 5, position, ForceMode.Impulse); //todo: Hardcoded Values

            Presenter.OnBallCollision();
            //todo: вынести _structureView в презентер
            //Presenter.OnBallCollision(direction, position);
        }

        public void OnDeactivatorCollision() =>
            Presenter.OnDeactivatorCollision();

        public void ReturnToPool()
        {
            ResetPresenter();
            _structureView = null;
            _pool.Release(this);
        }

        public void Initialize(StructureView structureView) =>
            _structureView = structureView;

        public void SetPool(IPool<CubeView> pool) =>
            _pool = pool;
    }
}
