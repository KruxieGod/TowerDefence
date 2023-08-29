using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class EnemySpawner : TileContent, IUpdatable
{
    public override TypeOfTile TileType => TypeOfTile.SpawnerEnemy;
    private Enemy _prefab;
    private HashSet<Enemy> _enemies = new HashSet<Enemy>();
    private List<Wave> _waves;
    private int _indexWave = 0;
    private IReadOnlyList<EnemyType> _currentWave;
    private float _timeToWave = 0;
    private float _timeToEnemy = 0;
    private int _countEnemiesWave = 0;
    private UnityEvent _moveOnEnemy;
    public override bool IsEnded => _indexWave >= _waves.Count && _countEnemiesWave <= 0 && _enemies.Count == 0;
    
    public EnemySpawner InitializeWave(Enemy prefab,List<Wave> waves)
    {
        _moveOnEnemy = new UnityEvent();
        _waves = waves;
        _prefab = prefab;
        return this;
    } 
    
    public void UpdateEntity()
    {
        if (_timeToWave <= 0 && _indexWave < _waves.Count)
        {
            var wave = _waves[_indexWave++];
            _currentWave = wave.WaveEnemy;
            _countEnemiesWave = _currentWave.Count;
            _timeToWave = wave.RecoverTimeWave;
        }

        if (_countEnemiesWave <= 0)
            _timeToWave -= Time.deltaTime;
        else if (_timeToEnemy <= 0)
        {
            var enemyBehaviour = _currentWave[_currentWave.Count - _countEnemiesWave--].GetBehaviour();
            _timeToEnemy = _waves[_indexWave - 1].RecoverTimeEnemies;
            var enemy = Instantiate(_prefab);
            enemy.Initialize(enemyBehaviour,SpawnerTile,Remove);
            _moveOnEnemy.AddListener(enemy.UpdatePos);
            _enemies.Add(enemy);
        }
        else
            _timeToEnemy -= Time.deltaTime;
        _moveOnEnemy.Invoke();
    }
    
    private void Remove(Enemy enemy) => _enemies.Remove(enemy);
}
