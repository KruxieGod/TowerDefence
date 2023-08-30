using System;
using UnityEngine;

[Serializable]
public struct EnemyInfo
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private BehaviourEnemy _behaviour;

    public EnemyType EnemyType => _enemyType;
    public BehaviourEnemy Behaviour => _behaviour;
}