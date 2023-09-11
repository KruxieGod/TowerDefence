using System.Collections;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AppearingWindowLoader : AssetLoader
{
    private const float SPEED_APPEARING = 2f;
    public async void LoadState(IInterface popUpWindow)
    {
        if (!_cachedObject.IsUnityNull())
            return;
        var gameResult = await Load<GameResult>(popUpWindow.AddressableName);
        gameResult.Initialize(popUpWindow);
        gameResult.StartCoroutine(
            UpdateAlpha(
                popUpWindow.GetCanvasGroup(gameResult)
                )
            );
    }

    private IEnumerator UpdateAlpha(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        while (canvasGroup != null &&
               canvasGroup.alpha <= 1f)
        {
            canvasGroup.alpha += SPEED_APPEARING * Time.deltaTime;
            yield return null;
        }
    }
}