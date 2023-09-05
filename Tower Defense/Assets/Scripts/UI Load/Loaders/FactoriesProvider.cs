
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class FactoriesProvider
{
    public GameFactories GameFactories { get; private set; }
    public async UniTask Load()
    {
        var obj = await Addressables.InstantiateAsync(AddressableData.GAMEFACTORIES);
        if (obj.TryGetComponent(out GameFactories gameFactories))
            GameFactories = gameFactories;
        else
            throw new ArgumentException("GameFactories is not found");
    }
}