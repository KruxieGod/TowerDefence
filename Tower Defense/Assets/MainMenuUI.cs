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
public class MainMenuUI : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private Button _createNewGame;
    private GameSaverProvider _gameSaverProvider;
    private Camera _uiCamera;
    private Lazy<LevelsScreenUI> _levelsScreenUI;
    private async void  Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = _uiCamera;
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
        Camera uiCamera,
        Lazy<LevelsScreenUI> levelsScreenUI)
    {
        _gameSaverProvider = gameSaverProvider;
        _uiCamera = uiCamera;
        _levelsScreenUI = levelsScreenUI;
    }

    private void CreateNewGame(int index) =>
        _levelsScreenUI.Value.Initialize(_gameSaverProvider.CreateNewGame(index).CompletedLevels);

    private void LoadLevels(int index) =>
        _levelsScreenUI.Value.Initialize(_gameSaverProvider.GetLevels(index).CompletedLevels);
}
