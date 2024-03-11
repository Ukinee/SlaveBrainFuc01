﻿using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Forms.CQRS.Queries;
using Codebase.Forms.Presentations.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.Game.CQRS.Queries;
using Codebase.Game.Models;
using Codebase.Game.Presentations.Implementations;
using Codebase.Game.Services.Interfaces;
using Codebase.Game.Views.Implementations;

namespace Codebase.Game.Services.Implementations.CreationServices
{
    public class LevelSelectorCreationService
    {
        private readonly AssetProvider _assetProvider;
        private readonly GetLevelIdsQuery _getLevelIdsQuery;
        private readonly GetFormVisibilityQuery _getFormVisibilityQuery;
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly ILevelViewRepository _levelViewRepository;
        private readonly IInterfaceView _interfaceView;
        private readonly string _assetPath;

        public LevelSelectorCreationService
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            ILevelViewRepository levelViewRepository,
            IInterfaceView interfaceView
        )
        {
            _assetProvider = assetProvider;
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _levelViewRepository = levelViewRepository;
            _interfaceView = interfaceView;
            _getLevelIdsQuery = new GetLevelIdsQuery(entityRepository);
            _assetPath = filePathProvider.Forms.Data[PathConstants.Forms.LevelSelectingFormView];
        }
        
        public int Create()
        {
            int id = _idGenerator.Generate();

            LevelSelectionFormModel model = new LevelSelectionFormModel(false, id);
            _entityRepository.Register(model);
            
            LevelSelectingFormView view = _assetProvider.Get<LevelSelectingFormView>(_assetPath);

            FormVisibilityPresenter visibilityPresenter = new FormVisibilityPresenter
            (
                id,
                _getFormVisibilityQuery,
                view
            );

            LevelSelectingFormPresenter levelSelectingFormPresenter = new LevelSelectingFormPresenter
            (
                id,
                _getLevelIdsQuery,
                _levelViewRepository,
                view
            );
            
            _interfaceView.SetChild(view);
            
            visibilityPresenter.Enable();
            levelSelectingFormPresenter.Enable();

            return id;
        }
    }
}