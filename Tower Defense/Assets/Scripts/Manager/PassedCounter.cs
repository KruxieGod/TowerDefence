using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassedCounter
{
    [SerializeField] private int _lives;
    private int _livesLast;
    private GameManager _gameManager;
    public static Action<int> NotifyCounterOn;

    public void Initialize(GameManager gameManager)
    {
        _livesLast = _lives;
        _gameManager = gameManager;
    }

    public void EnemyPassed(int hp)
    {
        if ((_livesLast -= hp) <= 0)
            _gameManager.ResetGame();
    }
}
