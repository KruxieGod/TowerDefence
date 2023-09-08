using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class LevelsScreen : MonoBehaviour
{
    private Canvas _canvas;

    public void Initialize(int countCompletedLevels)
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = ProjectContext.Instance.UiCamera;
        var settings = GetComponentsInChildren<LevelSettings>()
            .OrderBy(x => x.name)
            .ToArray();
        Debug.Log(settings.Length);
        for (int i = 0; i < countCompletedLevels; i++)
        {
            var button = settings[i].GetComponent<Button>();
            // ReSharper disable once AccessToModifiedClosure
            int index = i;
            button.onClick.AddListener(() => LoadLevel(settings[index]));
        }

        for (int i = countCompletedLevels == 0 ?
                 1:
                 countCompletedLevels;
             i < settings.Length;
             i++)
            settings[i].gameObject.SetActive(false);
    }

    private void LoadLevel(LevelSettings levelSettings)
    {
        Debug.Log(levelSettings.ScenarioNumber);
        var queue = new Queue<ILoadingOperation>();
        ((ISettable)ProjectContext.Instance.GameProvider).Set(levelSettings);
        queue.Enqueue(new SceneProvider(SceneData.GAMESCENE));
        ProjectContext.Instance.LoadingScreenLoader.LoadAndDestroy(queue);
    }
}
