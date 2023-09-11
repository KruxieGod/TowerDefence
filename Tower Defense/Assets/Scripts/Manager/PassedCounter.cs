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
    public static Action<int> NotifyCounterOn { get; protected set; }

    public void Initialize(GameManager gameManager)
    {
        _livesLast = _lives;
        _gameManager = gameManager;
    }

    public void EnemyPassed(int hp)
    {
        Debug.Log("EnemyPassed");
        if ((_livesLast -= hp) <= 0)
            ProjectContext.Instance.GameEvents.OnGameState(new DefeatLoader(_gameManager));
    }

    public void SetEvent()
    {
        Debug.Log("SetEvent");
        NotifyCounterOn -= EnemyPassed;
        NotifyCounterOn += EnemyPassed;
    }
}
