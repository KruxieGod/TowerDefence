
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProvider : ILoadingOperation
{
    private string _sceneName;
    public string Description => _sceneName + " loading...";

    private GameObjectsProvider _gameObjectsProvider;
    
    public virtual async UniTask Load(Action<float> onProcess)
    {
        var currentScene = SceneManager.GetActiveScene();
        onProcess?.Invoke(0.5f);
        var scene = SceneManager.LoadSceneAsync(_sceneName,
            LoadSceneMode.Additive);
        await scene;
        if (_gameObjectsProvider is not null)
             await _gameObjectsProvider.Load();
        onProcess?.Invoke(1);
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public SceneProvider(string sceneName,GameObjectsProvider gameObjectsProvider)
    {
        _sceneName = sceneName;
        _gameObjectsProvider = gameObjectsProvider;
    }
}