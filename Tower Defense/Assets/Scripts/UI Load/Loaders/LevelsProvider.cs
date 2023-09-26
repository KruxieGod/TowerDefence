
using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class LevelsProvider : AssetLoader
{
    public LevelsScreenUI LevelsScreenUI { get; private set; }
    public async UniTask<LevelsScreenUI> Load(DiContainer container)
    {
        LevelsScreenUI = await LoadAsync<LevelsScreenUI>(AddressableData.LEVELSCREEN);
        LevelsScreenUI.gameObject.SetActive(false);
        container.Inject(LevelsScreenUI);
        return LevelsScreenUI;
    }
}