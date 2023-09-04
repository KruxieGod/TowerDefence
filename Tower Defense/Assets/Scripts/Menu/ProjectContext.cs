using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    public Camera UiCamera => _mainCamera;
    public LoadingScreenProvider LoadingScreenProvider { get; private set; }
    public static ProjectContext Instance { get; private set; }
    public AssetProvider AssetProvider { get; private set; }
    public FactoriesProvider FactoriesProvider { get; private set; }
    public ScenariosLoader ScenariosLoader { get;private set; }

    public void Initialize()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        ScenariosLoader = new ScenariosLoader();
        FactoriesProvider = new FactoriesProvider();
        AssetProvider = new AssetProvider();
        LoadingScreenProvider = new LoadingScreenProvider();
    }
}
