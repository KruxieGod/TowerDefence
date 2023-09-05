
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneProvider : ILoadingOperation
{
    private string _sceneName;
    public string Description => _sceneName + " loading...";
    public async UniTask Load(Action<float> onProcess)
    {
        var currentScene = SceneManager.GetActiveScene();
        onProcess?.Invoke(0.5f);
        Debug.Log("Main menu in");
        var scene = SceneManager.LoadSceneAsync(_sceneName,
            LoadSceneMode.Additive);
        await scene;
        Debug.Log("Main menu is initialized");
        onProcess?.Invoke(1);
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public SceneProvider(string sceneName)
    {
        _sceneName = sceneName;
    }
}