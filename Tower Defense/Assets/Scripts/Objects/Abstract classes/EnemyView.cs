using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
    private const string deathFlag = "IsDead";
    private Animator _animator;
    private static readonly int IsDead = Animator.StringToHash(deathFlag);

    public void Awake() => _animator = GetComponent<Animator>();

    public void DieAnimation(ICollector enemy)
    {
        enemy.Recycle();
        _animator.SetBool(IsDead,true);
    }
}
