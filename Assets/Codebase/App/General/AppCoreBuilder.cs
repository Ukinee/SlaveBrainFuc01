using System;
using System.Collections.Generic;
using ApplicationCode.Core.Infrastructure.IdGenerators;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.App.Infrastructure.Builders;
using Codebase.App.Infrastructure.Builders.Pools;
using Codebase.App.Infrastructure.Builders.States;
using Codebase.App.Infrastructure.StateMachines;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Frameworks.EnitySystem.Repositories;
using Codebase.Core.Infrastructure.Curtain;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.Core.Services.AudioService.Implementation;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.SceneLoadServices;
using Codebase.Cubes.Services.Implementations;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Services.Implementations;
using UnityEngine;

namespace Codebase.App.General
{
    public class AppCoreBuilder
    {
        public AppCore Build()
        {
            ContextActionService contextActionService = new ContextActionService();
            SceneLoadService sceneLoadService = new SceneLoadService();
            AssetProvider assetProvider = new AssetProvider();

            FilePathProvider filePathProvider = new FilePathProviderFactory().Load();

            ICurtain curtain = assetProvider.Instantiate<Curtain>
                (filePathProvider.General.Data[PathConstants.General.Curtain]);

            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            IdGenerator idGenerator = new IdGenerator(1000);
            EntityRepository entityRepository = new EntityRepository();

            InitialSceneStateFactory initialSceneStateFactory = new InitialSceneStateFactory();

            AudioService audioService = new AudioServiceFactory().Create();
            
            
            #region Player

            PlayerIdProvider playerIdProvider = new PlayerIdProvider();
            PlayerPrefsSaveLoadService playerPrefsSaveLoadService = new PlayerPrefsSaveLoadService();
            GetPlayerDataObjectQuery getPlayerDataObjectQuery = new GetPlayerDataObjectQuery(entityRepository);

            DataService dataService = new DataService
                (playerPrefsSaveLoadService, playerIdProvider, getPlayerDataObjectQuery);

            PlayerCreationService playerCreationService = new PlayerCreationService
            (
                idGenerator,
                entityRepository,
                dataService
            );

            int playerId = playerCreationService.Create();
            playerIdProvider.Set(playerId);

            #endregion

            MainMenuSceneFactory mainMenuSceneFactory = new MainMenuSceneFactory
            (
                idGenerator,
                entityRepository,
                audioService,
                assetProvider,
                filePathProvider,
                playerIdProvider
            );

            GameplaySceneStateFactory gameplaySceneStateFactory = new GameplaySceneStateFactory
            (
                entityRepository,
                contextActionService,
                filePathProvider,
                assetProvider,
                idGenerator,
                new BallViewPool(new BallViewFactory(assetProvider, filePathProvider).Create),
                new CubeViewPool(new CubeViewFactory(assetProvider, filePathProvider).Create),
                audioService,
                playerIdProvider,
                dataService
            );

            var stateFactories = new Dictionary<Type, Func<IStateMachineService<IScenePayload>, IScenePayload, ISceneState>>()
            {
                [typeof(MainMenuScenePayload)] = mainMenuSceneFactory.CreateSceneState,
                [typeof(InitialScenePayload)] = initialSceneStateFactory.CreateSceneState,
                [typeof(GameplayScenePayload)] = gameplaySceneStateFactory.CreateSceneState,
            };

            SceneStateMachineService sceneStateMachineService = new SceneStateMachineService
            (
                sceneLoadService,
                curtain,
                stateFactories
            );

            appCore.Construct(sceneStateMachineService);
            appCore.Init();

            return appCore;
        }
    }
}
