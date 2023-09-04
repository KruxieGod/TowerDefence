
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class FactoriesProvider : ILoadingOperation
{
    public GameFactories GameFactories { get; private set; }
    public string Description => "Enemies factories loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.5f);
        var obj = await Addressables.InstantiateAsync(AddressableData.GAMEFACTORIES);
        if (obj.TryGetComponent(out GameFactories gameFactories))
            GameFactories = gameFactories;
        else
            throw new ArgumentException("GameFactories is not found");
        onProcess?.Invoke(1f);
    }
}