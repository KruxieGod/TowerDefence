
using System;
using Cysharp.Threading.Tasks;

public class GameProvider : ILoadingOperation
{
    public ScenariosProvider ScenariosProvider { get; private set; } = new();
    public FactoriesProvider FactoriesProvider { get; private set; } = new();
    public GameSaverProvider GameSaverProvider { get; private set; } = new();
    public TowerInfoLoader TowerInfoLoader { get; private set; } = new ();
    public string Description => "Game specifications loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        foreach (var operation in 
                 new ILoadingOperation[]
                 {
                     ScenariosProvider,
                     FactoriesProvider,
                     GameSaverProvider,
                     TowerInfoLoader
                 })
        {
            await operation.Load(onProcess);
        }
    }
}