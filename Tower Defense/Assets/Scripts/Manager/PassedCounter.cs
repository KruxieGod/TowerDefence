using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PassedCounter
{
    private int _lives = 1;
    private int _livesLast;
    public static Action<int> NotifyCounterOn { get; private set; }
    [Inject] private DefeatLoader _defeatLoader;
    [Inject] private AppearingWindowLoader _loader;
    public void Initialize()
    {
        _livesLast = _lives;
    }

    public void EnemyPassed(int hp)
    {
        Debug.Log("EnemyPassed");
        if ((_livesLast -= hp) <= 0)
            _loader.LoadState( _defeatLoader.Initialize());
    }

    public void SetEvent()
    {
        Debug.Log("SetEvent");
        NotifyCounterOn -= EnemyPassed;
        NotifyCounterOn += EnemyPassed;
    }
}
