
using System;
using Cysharp.Threading.Tasks;

public class TilesCounterUILoader : AssetLoader,ILoadingOperation
{
    public TilesCounter TilesCounter { get; private set; }
    public TilesCounterUI TilesCounterUI { get; private set; }
    public string Description => "UI Tiles is loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);
        TilesCounter = new TilesCounter(ProjectContext.Instance.GameProvider.ScenariosProvider.GetCurrentScenario().CountTiles);
        TilesCounterUI = await LoadAsync<TilesCounterUI>(AddressableData.UI_TILES);
        TilesCounterUI.Initialize(
            ProjectContext.Instance.GameProvider.ScenariosProvider.GetCurrentScenario().CountTiles);
        onProcess?.Invoke(1f);
    }
}