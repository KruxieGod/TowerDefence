using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class LevelsScreenUI : MonoBehaviour
{
    private Canvas _canvas;
    private LoadingScreenLoader _loadingScreenLoader;
    private GameSaverProvider _gameProvider;
    private Camera _uiCamera;
    private GameSceneLoader _gameSceneLoader;
    public void Initialize(int countCompletedLevels)
    {
        gameObject.SetActive(true);
        countCompletedLevels++;
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = _uiCamera;
        var settings = GetComponentsInChildren<LevelSettings>()
            .OrderBy(x => x.name)
            .ToArray();
        Debug.Log(settings.Length);
        for (int i = 0; i < countCompletedLevels && i < settings.Length; i++)
        {
            var button = settings[i].GetComponent<Button>();
            // ReSharper disable once AccessToModifiedClosure
            int index = i;
            button.onClick.AddListener(() => LoadLevel(settings[index]));
        }

        for (int i = countCompletedLevels;
             i < settings.Length;
             i++)
            settings[i].gameObject.SetActive(false);
    }

    [Inject]
    private void Construct(GameSaverProvider gameProvider,
        LoadingScreenLoader loadingScreenLoader,
        Camera uiCamera,
        GameSceneLoader gameSceneLoader)
    {
        _uiCamera = uiCamera;
        _gameProvider = gameProvider;
        _loadingScreenLoader = loadingScreenLoader;
        _gameSceneLoader = gameSceneLoader;
    }

    private void LoadLevel(LevelSettings levelSettings)
    {
        Debug.Log(levelSettings.ScenarioNumber);
        var queue = new Queue<ILoadingOperation>();
        ((ISettable<LevelSettings>)_gameProvider).Set(levelSettings);
        queue.Enqueue(_gameSceneLoader);
        _loadingScreenLoader.LoadAndDestroy(queue);
    }
}
