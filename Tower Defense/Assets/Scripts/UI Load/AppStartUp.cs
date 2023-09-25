using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class AppStartUp : MonoBehaviour
{
    private AssetProvider _assetProvider;
    private LoginOperation _loginOperation;
    private GameProvider _gameProvider;
    private MainMenuSceneProvider _mainMenuSceneProvider;
    private LoadingScreenLoader _loadingScreenLoader;
    private async void Start()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(_assetProvider);
        queue.Enqueue(_loginOperation);
        queue.Enqueue(_gameProvider);
        queue.Enqueue(_mainMenuSceneProvider);
        await _loadingScreenLoader.LoadAndDestroy(queue);
    }

    [Inject]
    private void Construct(
        AssetProvider assetProvider,
        LoginOperation loginOperation,
        GameProvider gameProvider,
        MainMenuSceneProvider mainMenuSceneProvider,
        LoadingScreenLoader loadingScreenLoader)
    {
        _assetProvider = assetProvider;
        _loginOperation = loginOperation;
        _gameProvider = gameProvider;
        _mainMenuSceneProvider = mainMenuSceneProvider;
        _loadingScreenLoader = loadingScreenLoader;
    }
}
