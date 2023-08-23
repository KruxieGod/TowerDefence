using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Wave
{
    [SerializeField] private List<EnemyType> _waveEnemy;
    public IReadOnlyList<EnemyType> WaveEnemy => _waveEnemy;
    [SerializeField] private float _recoverTimeWave;
    public float RecoverTimeWave => _recoverTimeWave;
    [SerializeField] private float _recoverTimeEnemies;
    public float RecoverTimeEnemies => _recoverTimeEnemies;
}