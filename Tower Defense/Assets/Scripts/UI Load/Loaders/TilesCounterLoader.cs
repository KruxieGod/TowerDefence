
using System;
using Cysharp.Threading.Tasks;

public class TilesCounterLoader : AssetLoader,ILoadingOperation
{
    public TilesCounter TilesCounter { get; private set; }
    public TilesCounterUI TilesCounterUI { get; private set; }
    private ScenariosProvider _scenariosProvider;
    public string Description => "UI Tiles is loading...";
    
    public TilesCounterLoader(ScenariosProvider scenariosProvider)
    {
        _scenariosProvider = scenariosProvider;
    }
    
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);
        TilesCounter = new TilesCounter(_scenariosProvider .GetCurrentScenario().CountTiles);
        TilesCounterUI = await LoadAsync<TilesCounterUI>(AddressableData.UI_TILES);
        TilesCounterUI.Initialize(
            _scenariosProvider.GetCurrentScenario().CountTiles);
        onProcess?.Invoke(1f);
    }
}