using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup),typeof(Canvas))]
public class GameResult : MonoBehaviour
{
    [SerializeField] private Button _toMenu;
    [SerializeField] private Button _nextAct;
    private MainMenuSceneProvider _mainMenuSceneProvider;
    private LoadingScreenLoader _loadingScreenLoader;
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = ProjectContexter.Instance.UiCamera;
    }

    [Inject]
    private void Construct(MainMenuSceneProvider mainMenuSceneProvider,
        LoadingScreenLoader loadingScreenLoader)
    {
        _mainMenuSceneProvider = mainMenuSceneProvider;
        _loadingScreenLoader = loadingScreenLoader;
    }
    
    private void ToMenu()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(_mainMenuSceneProvider);
        _loadingScreenLoader.LoadAndDestroy(queue);
    }

    public void Initialize(IInterface state)
    {
        _toMenu.onClick.AddListener(ToMenu);
        _nextAct.onClick.AddListener(state.ToNext);
    }
}
