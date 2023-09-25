using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class MenuTest : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private Button _createNewGame;
    private LoadingScreenLoader _loadingScreenLoader;
    private GameSaverProvider _gameSaverProvider;
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = ProjectContexter.Instance.UiCamera;
        int countGames = _gameSaverProvider.GetCurrentGames();
        _createNewGame.onClick.AddListener(() => CreateNewGame(countGames));
        Debug.Log("Games: "+ countGames);
        for (int i = 0; i < countGames; i++)
        {
            int index = i%_buttons.Count;
            _buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Load Game";
            _buttons[index].onClick.AddListener(() => LoadLevels(index));
        }
    }

    [Inject]
    private void Construct(GameSaverProvider gameSaverProvider,
        LoadingScreenLoader loadingScreenLoader)
    {
        _loadingScreenLoader = loadingScreenLoader;
        _gameSaverProvider = gameSaverProvider;
    }

    private void CreateNewGame(int index)
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new LevelsProvider(_gameSaverProvider.CreateNewGame(index)));
        _loadingScreenLoader.LoadAndDestroy(queue);
    }

    private void LoadLevels(int index)
    {
        Debug.Log("Load:" + index.ToString());
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new LevelsProvider( _gameSaverProvider.GetLevels(index)));
        _loadingScreenLoader.LoadAndDestroy(queue);
    }
}
