
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MainMenuLoader : ILoadingOperation
{
    public string Description => "Main menu loading...";
    
    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.5f);
        Debug.Log("Main menu in");
        var scene = SceneManager.LoadSceneAsync(SceneData.MAINMENUSCENE,
            LoadSceneMode.Additive);
        await scene;
        Debug.Log("Main menu is initialized");
        onProcess?.Invoke(1);
    }
}