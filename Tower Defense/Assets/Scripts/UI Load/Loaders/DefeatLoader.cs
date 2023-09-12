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
        Object.Destroy(_prefab.gameObject);
        _gameManager.StartNewGame();
    }

    public string AddressableName => "Defeat";
}