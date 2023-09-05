
using System;
using Cysharp.Threading.Tasks;

public class GameProvider : ILoadingOperation
{
    public ScenariosProvider ScenariosProvider { get; private set; } = new ScenariosProvider();
    public FactoriesProvider FactoriesProvider { get; private set; } = new FactoriesProvider();
    public string Description => "Game specifications loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.3f);
        await ScenariosProvider.Load();
        onProcess?.Invoke(0.6f);
        await FactoriesProvider.Load();
        onProcess?.Invoke(1f);
    }
}