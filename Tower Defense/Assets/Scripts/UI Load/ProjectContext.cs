using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    public Camera UiCamera => _mainCamera;
    public LoadingScreenLoader LoadingScreenLoader { get; private set; }
    public static ProjectContext Instance { get; private set; }
    public AssetProvider AssetProvider { get; private set; }
    public GameProvider GameProvider { get; private set; }

    public void Initialize()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        GameProvider = new GameProvider();
        AssetProvider = new AssetProvider();
        LoadingScreenLoader = new LoadingScreenLoader();
    }
}
