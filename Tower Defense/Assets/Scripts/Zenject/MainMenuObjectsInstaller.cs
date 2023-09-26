using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MainMenuObjectsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var provider = new LevelsProvider();
        provider.Load(Container);
        Container.Bind<Lazy<LevelsScreenUI>>().FromInstance(new Lazy<LevelsScreenUI>(() => provider .LevelsScreenUI));
        Container.Bind<GameSceneLoader>().ToSelf().AsSingle();
        Container.Bind<GameObjectsProvider>().ToSelf().AsSingle();
    }
}
