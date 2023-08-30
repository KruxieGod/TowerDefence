using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
    [SerializeField] private List<EnemyInfo> _waveEnemy;
    [SerializeField] private float _recoverTimeEnemies;
    [SerializeField] private GameEnemyFactory _factory;

    public State GetScenario(EnemySpawner spawner) => new State(this,spawner);
    
    public struct State
    {
        private Wave _wave;
        private float _timeLast;
        private int _index;
        private EnemySpawner _enemySpawner;
        public State(Wave wave,EnemySpawner tile)
        {
            _wave = wave;
            _timeLast = 0;
            _index = 0;
            _enemySpawner = tile;
        }

        public bool WaveUpdate()
        {
            if (_timeLast <= 0 && _index < _wave._waveEnemy.Count)
            {
                _timeLast = _wave._recoverTimeEnemies;
                _enemySpawner.SpawnEnemy(_wave._waveEnemy[_index++],_wave._factory);
            }
            _timeLast -= Time.deltaTime;
            return _index >= _wave._waveEnemy.Count;
        }
    }
}