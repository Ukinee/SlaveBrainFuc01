﻿using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Cubes.Models;
using Codebase.Cubes.Services.Implementations;
using Codebase.Cubes.Views.Implementations;

namespace Codebase.Cubes.Repositories.Implementations
{
    public class CubeRepositoryController
    {
        private readonly CubeModelRepository _cubeModelRepository = new CubeModelRepository();
        private readonly CubeViewRepository _cubeViewRepository;

        public CubeRepositoryController(CubeViewRepository cubeViewRepository)
        {
            _cubeViewRepository = cubeViewRepository;
        }

        public int Count => _cubeModelRepository.Count;
        public event Action<int> OnCubeAmountChanged;

        public void Register(CubeModel model, CubeView view)
        {
            _cubeModelRepository.Register(model.Id, model);
            _cubeViewRepository.Register(model.Id, view);

            OnCubeAmountChanged?.Invoke(Count);
            model.Disposed += Remove;
        }

        private void Remove(int cubeId)
        {
            _cubeModelRepository.Get(cubeId).Disposed -= Remove;

            CubeView cubeView = _cubeViewRepository.Get(cubeId);
            cubeView.ReturnToPool();
            
            _cubeModelRepository.Remove(cubeId);
            _cubeViewRepository.Remove(cubeId);
            OnCubeAmountChanged?.Invoke(Count);
        }
    }
}