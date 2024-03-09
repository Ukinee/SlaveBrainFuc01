using System;
using System.Collections.Generic;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Services.Common;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Factories.Forms;
using Codebase.Forms.Models;
using Codebase.Forms.Presentations.Implementations.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Services.Implementations.Factories;
using Codebase.Forms.Views.Interfaces;

namespace Codebase.Forms.Factories
{
    public class FormCreationServiceFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IInterfaceService _interfaceService;
        private readonly IInterfaceView _interfaceView;
        private readonly IAudioService _audioService;
        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;

        public FormCreationServiceFactory
        (
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IInterfaceService interfaceService,
            IInterfaceView interfaceView,
            IAudioService audioService,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _interfaceService = interfaceService;
            _interfaceView = interfaceView;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
        }

        public FormCreationService Create()
        {
            MainMenuFormFactory mainMenuFormFactory = new MainMenuFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _assetProvider,
                _filePathProvider
            );

            MainMenuSettingsFormFactory settingsFormFactory = new MainMenuSettingsFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _audioService,
                _assetProvider,
                _filePathProvider
            );

            MainMenuShopFormFactory shopFormFactory = new MainMenuShopFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _assetProvider,
                _filePathProvider
            );

            MainMenuLeaderboardFormFactory leaderboardFormFactory = new MainMenuLeaderboardFormFactory
            (
                _idGenerator,
                _interfaceService,
                _entityRepository,
                _assetProvider,
                _filePathProvider
            );

            var factories = new Dictionary<Type, Func<Tuple<FormBase, IFormView>>>()
            {
                [typeof(MainMenuFormType)] = mainMenuFormFactory.Create,
                [typeof(MainMenuSettingsFormType)] = settingsFormFactory.Create,
                [typeof(MainMenuShopFormType)] = shopFormFactory.Create,
                [typeof(MainMenuLeaderboardFormType)] = leaderboardFormFactory.Create,
            };

            return new FormCreationService(_entityRepository, _interfaceView, _interfaceService, factories);
        }
    }
}
