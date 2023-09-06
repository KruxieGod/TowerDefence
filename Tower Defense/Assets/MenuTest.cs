using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        for (int i = 0; i < countGames; i++)
        {
            _buttons[i].onClick.AddListener(LoadLevels);
        }
    }

    private void CreateNewGame()
    {
        
    }

    private void LoadLevels()
    {
        
    }
}
