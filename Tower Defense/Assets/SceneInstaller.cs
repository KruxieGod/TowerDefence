using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Inject] private FactoriesProvider _factoriesProvider;
    [Inject] private ScenariosProvider _scenariosProvider;
    public override void InstallBindings()
    {
        Container.Bind<GameFactories>().FromInstance(_factoriesProvider.GameFactories).AsSingle();
        Container.Bind<GameTileFactory>().FromInstance(_factoriesProvider.GameFactories.GameTileFactory).AsSingle();
        Container.Bind<GameTowerFactory>().FromInstance(_factoriesProvider.GameFactories.GameTowerFactory).AsSingle();
        var currentScenario = _scenariosProvider.GetCurrentScenario();
        Container.Bind<GameScenarioJson>().FromInstance(currentScenario).AsSingle();
        Container.Inject(currentScenario);
    }
}
