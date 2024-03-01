using System;
using System.Collections.Generic;
using System.IO;
using ApplicationCode.Core.Services.AssetProviders;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.Infrastructure.Builders;
using Codebase.App.Infrastructure.Builders.Pools;
using Codebase.App.Infrastructure.Builders.States;
using Codebase.App.Infrastructure.StateMachines;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Infrastructure.Curtain;
using Codebase.Core.Services.NewInputSystem.General;
using Codebase.Core.Services.SceneLoadServices;
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
            
            InitialSceneStateFactory initialSceneStateFactory = new InitialSceneStateFactory();
            MainMenuSceneFactory mainMenuSceneFactory = new MainMenuSceneFactory();
            GameplaySceneStateFactory gameplaySceneStateFactory = new GameplaySceneStateFactory
            (
                contextActionService,
                filePathProvider,
                assetProvider,
                new BallPoolFactory(assetProvider, filePathProvider).Create(),
                new CubePoolServiceFactory(assetProvider, filePathProvider).Create(),
                new AudioServiceFactory().Create()
            );

            var stateFactories = new Dictionary<Type, Func<IStateMachineService, ISceneState>>()
            {
                [typeof(MainMenuScene)] = mainMenuSceneFactory.CreateSceneState,
                [typeof(InitialScene)] = initialSceneStateFactory.CreateSceneState,
                [typeof(GameplayScene)] = gameplaySceneStateFactory.CreateSceneState,
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
