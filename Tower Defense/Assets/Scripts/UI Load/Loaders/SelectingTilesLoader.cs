
using System;
using Cysharp.Threading.Tasks;

public class SelectingTilesLoader : AssetLoader,ILoadingOperation
{
    public SelectingTiles SelectingTiles { get; private set; }
    public string Description => "Tiles UI is loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        SelectingTiles = new SelectingTiles(
            ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTileFactory,
            ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTowerFactory);
        var selectingTilesUI = await Load<SelectingTilesUI>(AddressableData.SELECTING_TILES_UI);
        selectingTilesUI.Initialize(SelectingTiles);
        onProcess?.Invoke(1f);
    }
}