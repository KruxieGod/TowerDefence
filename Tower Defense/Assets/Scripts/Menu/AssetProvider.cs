
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class AssetProvider : ILoadingOperation
{
    public string Description => "Assets loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.5f);
        var objects = Addressables.InitializeAsync();
        await objects;
        onProcess?.Invoke(1f);
    }
}