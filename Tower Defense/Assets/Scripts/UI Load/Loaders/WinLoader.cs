
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoader : IInterface
{
    private readonly LoadingScreenLoader _loadingScreenLoader;
    public WinLoader(LoadingScreenLoader loadingScreenLoader,
        GameSaverProvider gameSaverProvider)
    {
        _loadingScreenLoader = loadingScreenLoader;
        gameSaverProvider.Save();
    }
    
    public CanvasGroup GetCanvasGroup(GameResult gameResult) => gameResult.GetComponent<CanvasGroup>();
    
    public void ToNext()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new SceneProvider(SceneData.MAINMENUSCENE));
        _loadingScreenLoader.LoadAndDestroy(queue);
    }

    public string AddressableName => "Win";
}