using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Balls.Models;
using Codebase.Balls.Presentations.Implementations;
using Codebase.Balls.Presentations.Interfaces;
using Codebase.Balls.Services.Interfaces;
using Codebase.Balls.Views;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.PoolTags;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class BallCreationService
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _path;
        private readonly ICollisionService _collisionService;
        private readonly IMoveService _moveService;
        private readonly BallPoolTag _gameObject;

        public BallCreationService
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            ICollisionService collisionService,
            IMoveService moveService
        )
        {
            _assetProvider = assetProvider;
            _collisionService = collisionService;
            _moveService = moveService;
            _path = filePathProvider.General.Data[PathConstants.General.Ball];

            _gameObject = Object.FindObjectOfType<BallPoolTag>()
                          ?? new GameObject(nameof(BallPool)).AddComponent<BallPoolTag>();

            Object.DontDestroyOnLoad(_gameObject);
        }

        public BallPresenter Create()
        {
            BallModel ballModel = new BallModel();
            BallView ballView = _assetProvider.Instantiate<BallView>(_path);
            BallPresenter ballPresenter = new BallPresenter(_collisionService, _moveService, ballModel, ballView);

            ballView.Construct(ballPresenter);
            ballView.transform.SetParent(_gameObject.transform);

            return ballPresenter;
        }
    }
}
