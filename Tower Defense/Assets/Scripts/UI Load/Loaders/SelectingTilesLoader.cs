
using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class SelectingTilesLoader : AssetLoader,ILoadingOperation
{
    private GameTileFactory _gameTileFactory;
    private GameTowerFactory _gameTowerFactory;
    [field : Inject]public SelectingTiles SelectingTiles { get; private set; }
    public string Description => "Tiles UI is loading...";

    public SelectingTilesLoader(SelectingTiles selectingTiles) => SelectingTiles = selectingTiles;
    
    public async UniTask Load(Action<float> onProcess)
    {
        var selectingTilesUI = await LoadAsync<SelectingTilesUI>(AddressableData.SELECTING_TILES_UI);
        selectingTilesUI.Initialize(SelectingTiles);
        onProcess?.Invoke(1f);
    }
}