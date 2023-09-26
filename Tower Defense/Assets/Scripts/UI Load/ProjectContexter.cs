using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProjectContexter : MonoBehaviour
{
    [field: SerializeField] public LayerMask LayerFloor { get; private set; }
    public static ProjectContexter Instance { get; private set; }
    [SerializeField] private Camera _mainCamera;
    public Camera UiCamera => _mainCamera;
    public AppearingWindowLoader AppearingWindowLoader { get; private set; }
    public GameEvents GameEvents { get; private set; }
    public GameObjectsProvider GameObjectsProvider { get; private set; }

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        GameObjectsProvider = new GameObjectsProvider();
        AppearingWindowLoader = new AppearingWindowLoader();
        GameEvents = new GameEvents(value =>
        {
            AppearingWindowLoader.LoadState(value);
            GameObjectsProvider.GameManager.EndGame();
        });
    }
}