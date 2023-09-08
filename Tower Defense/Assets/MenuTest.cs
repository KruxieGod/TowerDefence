using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MenuTest : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private Button _createNewGame;
    
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = ProjectContext.Instance.UiCamera;
        _createNewGame.onClick.AddListener(CreateNewGame);
        int countGames = ProjectContext.Instance.GameProvider.GameSaverProvider.GetCurrentGames();
        Debug.Log("Games: "+ countGames);
        for (int i = 0; i < countGames; i++)
        {
            _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Load Game";
            int index = i;
            _buttons[i].onClick.AddListener(() => LoadLevels(index));
        }
    }

    private void CreateNewGame()
    {
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new LevelsProvider(ProjectContext.Instance.GameProvider.GameSaverProvider.CreateNewGame()));
        ProjectContext.Instance.LoadingScreenLoader.LoadAndDestroy(queue);
    }

    private void LoadLevels(int index)
    {
        Debug.Log("Load:" + index.ToString());
        var queue = new Queue<ILoadingOperation>();
        queue.Enqueue(new LevelsProvider(ProjectContext.Instance.GameProvider.GameSaverProvider.GetLevels(index)));
        ProjectContext.Instance.LoadingScreenLoader.LoadAndDestroy(queue);
    }
}
