
using System;
using Cysharp.Threading.Tasks;

public class GameProvider : ILoadingOperation,ISettable
{
    public ScenariosProvider ScenariosProvider { get; private set; } = new ScenariosProvider();
    public FactoriesProvider FactoriesProvider { get; private set; } = new FactoriesProvider();
    public GameSaverProvider GameSaverProvider { get; private set; } = new GameSaverProvider();
    public string Description => "Game specifications loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.3f);
        await ScenariosProvider.Load();
        onProcess?.Invoke(0.6f);
        await FactoriesProvider.Load();
        onProcess?.Invoke(0.8f);
        await GameSaverProvider.Load();
        onProcess?.Invoke(1f);
    }

    public void Set(LevelSettings index)
    {
        throw new NotImplementedException();
    }
}