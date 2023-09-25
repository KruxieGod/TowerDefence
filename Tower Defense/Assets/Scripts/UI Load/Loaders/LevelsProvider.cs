
using System;
using Cysharp.Threading.Tasks;

public class LevelsProvider : AssetLoader,ILoadingOperation
{
    private LevelsSaveData _levels;
    public LevelsProvider(LevelsSaveData levels)
    {
        _levels = levels;
    }
    
    public string Description => "Levels is loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.3f);
        var levelsScreen = await LoadAsync<LevelsScreen>(AddressableData.LEVELSCREEN);
        levelsScreen.gameObject.SetActive(true);
        onProcess?.Invoke(1);
        //levelsScreen.Initialize( _levels.CompletedLevels);
    }
}