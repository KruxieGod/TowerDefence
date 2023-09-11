
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoader : IInterface
{
    public WinLoader()
    {
        ProjectContext.Instance.GameProvider.GameSaverProvider.Save();
    }
    
    public CanvasGroup GetCanvasGroup(GameResult gameResult) => gameResult.GetComponent<CanvasGroup>();
    
    public void ToNext()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new SceneProvider(SceneData.MAINMENUSCENE));
        ProjectContext.Instance.LoadingScreenLoader.LoadAndDestroy(queue);
    }

    public string AddressableName => "Win";
}