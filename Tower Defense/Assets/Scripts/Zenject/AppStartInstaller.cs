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
        var projectContexter = FindAnyObjectByType<ProjectContexter>();
        Container.Bind<Camera>().FromInstance(projectContexter.UiCamera);
    }
}