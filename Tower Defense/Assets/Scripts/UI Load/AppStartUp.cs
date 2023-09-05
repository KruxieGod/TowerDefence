using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStartUp : MonoBehaviour
{
    private LoadingScreenLoader LoadingScreenLoader => ProjectContext.Instance.LoadingScreenLoader;
    [SerializeField] private ProjectContext _projectContext;
    private void Start()
    {
        _projectContext.Initialize();
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(ProjectContext.Instance.AssetProvider);
        queue.Enqueue(new LoginOperation());
        queue.Enqueue(ProjectContext.Instance.GameProvider);
        queue.Enqueue(new SceneProvider(SceneData.MAINMENUSCENE));
        LoadingScreenLoader.LoadAndDestroy(queue);
    }
}
