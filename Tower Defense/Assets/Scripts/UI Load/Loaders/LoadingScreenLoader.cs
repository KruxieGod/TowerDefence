
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadingScreenLoader : AssetLoader
{
    public async UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
    {
        var loadingScreen = await LoadAsync<LoadingScreen>(AddressableData.LOADINGSCREEN);
        
        await loadingScreen.Load(loadingOperations);
        Unload();
    }

    private void Unload()
    {
        Debug.Log("UnLoad");
        if (_cachedObject == null)
            return;
        _cachedObject.SetActive(false);
        Addressables.ReleaseInstance(_cachedObject);
        _cachedObject = null;
    }
}