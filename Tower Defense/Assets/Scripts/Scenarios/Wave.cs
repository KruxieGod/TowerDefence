using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField] private List<EnemyInfo> _waveEnemy;
    public IReadOnlyList<EnemyInfo> WaveEnemy => _waveEnemy;
    [SerializeField] private float _recoverTimeEnemies;
    public float RecoverTimeEnemies => _recoverTimeEnemies;
    [SerializeField] private GameEnemyFactory _factory;

    public State GetScenario() => new State(_recoverTimeEnemies);
    
    public struct State
    {
        private readonly float _timeBetweenEnemies;
        private float _timeLast;

        public State(float timeBetweenEnemies)
        {
            _timeBetweenEnemies = timeBetweenEnemies;
            _timeLast = timeBetweenEnemies;
        }

        public void WaveUpdate()
        {
            if (_timeLast <= 0)
            {
                
            }

            _timeLast -= Time.deltaTime;
        }
    }
}