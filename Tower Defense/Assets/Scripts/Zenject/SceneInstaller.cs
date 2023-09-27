using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Inject] private FactoriesProvider _factoriesProvider;
    [Inject] private ScenariosProvider _scenariosProvider;
    [Inject] private GameObjectsProvider _gameObjectsProvider;
    [Inject] private SelectingTilesLoader _selectingTilesLoader;
    [Inject] private CounterMoneyLoader _counterMoneyLoader;
    [Inject] private TilesCounterLoader _tilesCounterLoader;
    public override void InstallBindings()
    {
        Container.Bind<GameFactories>().FromInstance(_factoriesProvider.GameFactories).AsSingle();
        Container.Bind<GameTileFactory>().FromInstance(_factoriesProvider.GameFactories.GameTileFactory).AsSingle();
        Container.Bind<GameTowerFactory>().FromInstance(_factoriesProvider.GameFactories.GameTowerFactory).AsSingle();
        Container.Bind<Lazy<GameManager>>().FromInstance(new Lazy<GameManager>(() => _gameObjectsProvider.GameManager)).AsSingle();
        Container.Bind<SelectingTiles>().ToSelf().AsSingle();
        Container.Inject(_selectingTilesLoader);
        Container.Bind<DefeatLoader>().ToSelf().AsSingle();
        Container.Bind<WinLoader>().ToSelf().AsSingle();
        Container.Bind<AppearingWindowLoader>().ToSelf().AsSingle();
        Container.Bind<PassedCounter>().ToSelf().AsSingle();
        Container.Bind<TilesCounter>().FromInstance(_tilesCounterLoader.TilesCounter).AsSingle();
        Container.Bind<CounterMoney>().FromInstance(_counterMoneyLoader.CounterMoney).AsSingle();
        Container.Inject(_factoriesProvider.GameFactories.GameTowerFactory);
        var currentScenario = _scenariosProvider.GetCurrentScenario();
        Container.Bind<GameScenarioJson>().FromInstance(currentScenario).AsSingle();
        Container.Inject(currentScenario);
    }
}
