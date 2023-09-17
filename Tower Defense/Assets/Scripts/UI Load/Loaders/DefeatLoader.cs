using Unity.VisualScripting;
using UnityEngine;

public class DefeatLoader : IInterface
{
    private GameResult _prefab;
    private GameManager _gameManager;
    public DefeatLoader(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public CanvasGroup GetCanvasGroup(GameResult gameResult) => (_prefab = gameResult).GetComponent<CanvasGroup>();
    public void ToNext()
    {
        ProjectContext.Instance.GameSceneLoader.CounterMoneyLoader.CounterMoney.Reset();
        _prefab.gameObject.Destroy();
        Debug.Log("ToNext : "+_prefab.IsUnityNull());
        _gameManager.StartNewGame();
    }

    public string AddressableName => "Defeat";
}