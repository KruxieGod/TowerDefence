using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu,Serializable]
public class Wave : ScriptableObject
{
    [SerializeField] private List<EnemyInfo> _waveEnemy;
    [SerializeField] private float _recoverTimeEnemies;
    [SerializeField] private GameEnemyFactory _factory;

    public WaveJson GetJsonClass()
    {
        return new WaveJson(_waveEnemy,_recoverTimeEnemies,_factory.name);
    }

    private void OnValidate()
    {
        for (int i = 0; i < _waveEnemy.Count; i++)
            _waveEnemy[i] = _waveEnemy[i].OnValidate();
    }
}

[Serializable]
public class WaveJson
{
    [SerializeField]private List<EnemyInfo> _waveEnemy;
    [SerializeField]private float _recoverTimeEnemies;
    [SerializeField] private string _factory;
    [NonSerialized]private GameEnemyFactory _enemyFactory;
    public WaveJson(List<EnemyInfo> waveEnemy,
        float recoverTimeEnemies,
        string name)
    {
        _factory = name;
        _waveEnemy = waveEnemy;
        _recoverTimeEnemies = recoverTimeEnemies;
    }
    
    public State GetScenario(EnemySpawner spawner,GameFactories gameFactories)
    {
        _enemyFactory = gameFactories.GetEnemyFactoryFrom(_factory);
        return new State(this, spawner);
    }

    public struct State
    {
        private WaveJson _wave;
        private float _timeLast;
        private int _index;
        private EnemySpawner _enemySpawner;
        public State(WaveJson wave,EnemySpawner tile)
        {
            _wave = wave;
            _timeLast = 0;
            _index = 0;
            _enemySpawner = tile;
        }

        public bool WaveUpdate()
        {
            if (_wave is null)
                return true;
            if (_timeLast <= 0 && _index < _wave._waveEnemy.Count)
            {
                _timeLast = _wave._recoverTimeEnemies;
                _enemySpawner.SpawnEnemy(_wave._waveEnemy[_index++],_wave._enemyFactory);
            }
            _timeLast -= Time.deltaTime;
            return _index >= _wave._waveEnemy.Count;
        }
    }
}