using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup),typeof(Canvas))]
public class GameResult : MonoBehaviour
{
    [SerializeField] private Button _toMenu;
    [SerializeField] private Button _nextAct;
    
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = ProjectContext.Instance.UiCamera;
    }

    private void ToMenu()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new SceneProvider(SceneData.MAINMENUSCENE));
        ProjectContext.Instance
            .LoadingScreenLoader
            .LoadAndDestroy(queue);
    }

    public void Initialize(IInterface state)
    {
        _toMenu.onClick.AddListener(ToMenu);
        _nextAct.onClick.AddListener(state.ToNext);
    }
}
