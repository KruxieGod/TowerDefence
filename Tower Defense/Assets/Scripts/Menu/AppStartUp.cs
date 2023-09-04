using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStartUp : MonoBehaviour
{
    private LoadingScreenProvider _loadingScreenProvider => ProjectContext.Instance.LoadingScreenProvider;
    [SerializeField] private ProjectContext _projectContext;
    private void Start()
    {
        _projectContext.Initialize();
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(ProjectContext.Instance.AssetProvider);
        queue.Enqueue(new LoginOperation());
        queue.Enqueue(ProjectContext.Instance.FactoriesProvider);
        queue.Enqueue(new SceneLoader(SceneData.MAINMENUSCENE));
        _loadingScreenProvider.LoadAndDestroy(queue);
    }
}
