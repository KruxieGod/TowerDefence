
using System;
using Cysharp.Threading.Tasks;

public class TilesCounterLoader : AssetLoader,ILoadingOperation
{
    public TilesCounter TilesCounter { get; private set; }
    public TilesCounterUI TilesCounterUI { get; private set; }
    public string Description => "UI Tiles is loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);
        //TilesCounter = new TilesCounter(ProjectContexter.Instance.GameProvider.ScenariosProvider.GetCurrentScenario().CountTiles);
        TilesCounterUI = await LoadAsync<TilesCounterUI>(AddressableData.UI_TILES);
       // TilesCounterUI.Initialize(
          //  ProjectContexter.Instance.GameProvider.ScenariosProvider.GetCurrentScenario().CountTiles);
        onProcess?.Invoke(1f);
    }
}