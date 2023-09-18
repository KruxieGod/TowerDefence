using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [field: SerializeField] public LayerMask LayerFloor { get; private set; }
    public static ProjectContext Instance { get; private set; }
    [SerializeField] private Camera _mainCamera;
    public Camera UiCamera => _mainCamera;
    public LoadingScreenLoader LoadingScreenLoader { get; private set; }
    public AssetProvider AssetProvider { get; private set; }
    public GameProvider GameProvider { get; private set; }
    public AppearingWindowLoader AppearingWindowLoader { get; private set; }
    public GameEvents GameEvents { get; private set; }
    public GameObjectsProvider GameObjectsProvider { get; private set; }
    public GameSceneLoader GameSceneLoader { get; private set; }

    public void Initialize()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        GameSceneLoader = new GameSceneLoader();
        GameObjectsProvider = new GameObjectsProvider();
        AppearingWindowLoader = new AppearingWindowLoader();
        GameEvents = new GameEvents(value =>
        {
            AppearingWindowLoader.LoadState(value);
            GameObjectsProvider.GameManager.EndGame();
        });
        GameProvider = new GameProvider();
        AssetProvider = new AssetProvider();
        LoadingScreenLoader = new LoadingScreenLoader();
    }
}