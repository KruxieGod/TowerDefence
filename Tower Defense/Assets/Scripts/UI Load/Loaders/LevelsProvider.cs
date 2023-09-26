
using System;
using Cysharp.Threading.Tasks;

public class LevelsProvider : AssetLoader,ILoadingOperation
{
    public LevelsScreenUI LevelsScreenUI { get; private set; }
    public string Description => "Levels is loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.3f);
        LevelsScreenUI = await LoadAsync<LevelsScreenUI>(AddressableData.LEVELSCREEN);
        LevelsScreenUI.gameObject.SetActive(false);
        onProcess?.Invoke(1);
    }
}