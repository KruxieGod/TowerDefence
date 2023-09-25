
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoader : IInterface
{
    private readonly LoadingScreenLoader _loadingScreenLoader;
    private readonly MainMenuSceneProvider _mainMenuSceneProvider;
    public WinLoader(LoadingScreenLoader loadingScreenLoader,
        GameSaverProvider gameSaverProvider,
        MainMenuSceneProvider mainMenuSceneProvider)
    {
        _loadingScreenLoader = loadingScreenLoader;
        gameSaverProvider.Save();
        _mainMenuSceneProvider = mainMenuSceneProvider;
    }
    
    public CanvasGroup GetCanvasGroup(GameResult gameResult) => gameResult.GetComponent<CanvasGroup>();
    
    public void ToNext()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(_mainMenuSceneProvider);
        _loadingScreenLoader.LoadAndDestroy(queue);
    }

    public string AddressableName => "Win";
}