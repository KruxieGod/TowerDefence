using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuObjectsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var levelsProvider = new LevelsProvider();
        levelsProvider.Load(null);
        Container.Bind<LevelsScreenUI>().FromInstance(levelsProvider.LevelsScreenUI);
        Container.Inject(levelsProvider.LevelsScreenUI);
    }
}
