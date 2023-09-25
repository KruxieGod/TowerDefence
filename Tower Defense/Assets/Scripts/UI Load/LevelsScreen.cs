using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class LevelsScreen : MonoBehaviour
{
    private Canvas _canvas;
    private LoadingScreenLoader _loadingScreenLoader;
    private GameProvider _gameProvider;
    public void Initialize(int countCompletedLevels)
    {
        countCompletedLevels++;
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = ProjectContexter.Instance.UiCamera;
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
    private void Construct(GameProvider gameProvider,
        LoadingScreenLoader loadingScreenLoader)
    {
        _gameProvider = gameProvider;
        _loadingScreenLoader = loadingScreenLoader;
    }

    private void LoadLevel(LevelSettings levelSettings)
    {
        Debug.Log(levelSettings.ScenarioNumber);
        var queue = new Queue<ILoadingOperation>();
        ((ISettable<LevelSettings>)_gameProvider).Set(levelSettings);
        queue.Enqueue(ProjectContexter.Instance.GameSceneLoader);
        _loadingScreenLoader.LoadAndDestroy(queue);
    }
}
