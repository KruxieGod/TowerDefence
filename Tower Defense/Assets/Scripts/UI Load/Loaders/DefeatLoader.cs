using System;
using Unity.VisualScripting;
using UnityEngine;

public class DefeatLoader : IInterface
{
    private GameResult _prefab;
    private Lazy<GameManager> _gameManager;
    public DefeatLoader(Lazy<GameManager> gameManager)
    {
        _gameManager = gameManager;
    }
    
    public CanvasGroup GetCanvasGroup(GameResult gameResult) => (_prefab = gameResult).GetComponent<CanvasGroup>();
    public void ToNext()
    {
        //ProjectContexter.Instance.GameSceneLoader.CounterMoneyLoader.CounterMoney.Reset();
        _prefab.gameObject.Destroy();
        Debug.Log("ToNext : "+_prefab.IsUnityNull());
        _gameManager.Value.StartNewGame();
    }

    public DefeatLoader Initialize()
    {
        _gameManager.Value.EndGame();
        return this;
    }

    public string AddressableName => "Defeat";
}