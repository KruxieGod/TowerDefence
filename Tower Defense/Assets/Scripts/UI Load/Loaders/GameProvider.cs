
using System;
using Cysharp.Threading.Tasks;

public class GameProvider : ILoadingOperation
{
    private readonly ScenariosProvider _scenariosProvider;
    private readonly FactoriesProvider _factoriesProvider;
    private readonly GameSaverProvider _gameSaverProvider;
    private readonly TowerInfoLoader _towerInfoLoader;
    public string Description => "Game specifications loading...";

    public GameProvider(ScenariosProvider scenariosProvider,
    FactoriesProvider factoriesProvider,
    GameSaverProvider gameSaverProvider,
    TowerInfoLoader towerInfoLoader)
    {
        _scenariosProvider = scenariosProvider;
        _factoriesProvider = factoriesProvider;
        _gameSaverProvider = gameSaverProvider;
        _towerInfoLoader = towerInfoLoader;
    }
    public async UniTask Load(Action<float> onProcess)
    {
        foreach (var operation in 
                 new ILoadingOperation[]
                 {
                     _scenariosProvider,
                     _factoriesProvider,
                     _gameSaverProvider,
                     _towerInfoLoader
                 })
        {
            await operation.Load(onProcess);
        }
    }
}