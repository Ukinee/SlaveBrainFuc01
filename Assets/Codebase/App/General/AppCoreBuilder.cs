using System;
using System.Collections.Generic;
using ApplicationCode.Core.Services.AssetProviders;
using Assets.Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.App.BuildersFactories.States;
using Codebase.App.Infrastructure.Builders;
using Codebase.App.Infrastructure.StateMachines;
using Codebase.App.Infrastructure.StateMachines.States;
using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Infrastructure.Services.Implementations;
using Codebase.Core.Infrastructure.Services.Interfaces;
using Codebase.Core.Services.SceneLoadServices;
using UnityEngine;

namespace Codebase.App.General
{
    public class AppCoreBuilder
    {
        public AppCore Build()
        {
            SceneLoadService sceneLoadService = new SceneLoadService();
            AssetProvider assetProvider = new AssetProvider();

            FilePathProvider environmentDataBuilder = new FilePathProviderFactory().Load();
            
            ICurtain curtain = assetProvider.Instantiate<Curtain>
                (environmentDataBuilder.General.Data[PathConstants.General.Curtain]);

            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();
            
            MainMenuSceneFactory mainMenuSceneFactory = new MainMenuSceneFactory();
            InitialSceneStateFactory initialSceneStateFactory = new InitialSceneStateFactory();

            var stateFactories = new Dictionary<Type, Func<IStateMachineService, ISceneState>>()
            {
                [typeof(MainMenuScene)] = mainMenuSceneFactory.CreateSceneState,
                [typeof(InitialScene)] = initialSceneStateFactory.CreateSceneState,
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
