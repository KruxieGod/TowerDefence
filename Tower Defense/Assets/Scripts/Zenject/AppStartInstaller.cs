using UnityEngine;
using Zenject;

public class AppStartInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AssetProvider>().ToSelf().AsSingle();
        Container.Bind<LoginOperation>().ToSelf().AsSingle();
        Container.Bind<GameProvider>().ToSelf().AsSingle();
        Container.Bind<MainMenuSceneProvider>().ToSelf().AsSingle();
        Container.Bind<LoadingScreenLoader>().ToSelf().AsSingle();
        Container.Bind<ScenariosProvider>().ToSelf().AsSingle();
        Container.Bind<FactoriesProvider>().ToSelf().AsSingle();
        Container.Bind<GameSaverProvider>().ToSelf().AsSingle();
        Container.Bind<TowerInfoLoader>().ToSelf().AsSingle();
        Container.Bind<GameObjectsProvider>().ToSelf().AsSingle();
        BindGameScene();
        var projectContext = FindAnyObjectByType<ProjectContexter>();
        Container.Bind<Camera>().FromInstance(projectContext.UiCamera);
    }

    private void BindGameScene()
    {
        Container.Bind<GameSceneLoader>().ToSelf().AsSingle();
        Container.Bind<TilesCounterLoader>().ToSelf().AsSingle();
        Container.Bind<SelectingTilesLoader>().ToSelf().AsSingle();
        Container.Bind<CounterMoneyLoader>().ToSelf().AsSingle();
        Container.Bind<SelectingTiles>().FromInstance(null);
    }
}