using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MenuTest : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private Button _button;
    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = ProjectContext.Instance.UiCamera;
        _button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        Debug.Log("MenuTest");
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(ProjectContext.Instance.ScenariosLoader);
        queue.Enqueue(new SceneLoader(SceneData.GAMESCENE));
        ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(queue);
    }
}
