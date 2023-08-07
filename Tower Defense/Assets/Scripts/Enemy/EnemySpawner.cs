using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class EnemySpawner
{
    private Tile _spawnerTile;
    private Enemy _prefab;
    private List<Enemy> _enemies = new List<Enemy>();
    public IReadOnlyList<Enemy> Enemies => _enemies;
    private List<Wave> _waves;
    private int _indexWave = 0;
    private IReadOnlyList<EnemyType> _currentWave;
    private int _indexEnemy = 0;
    private float _timeToWave = 0;
    private float _timeToEnemy = 0;
    private int _countEnemiesWave = 0;
    private UnityEvent _moveOnEnemy;
    public EnemySpawner(Tile spawnerTile,Enemy prefab,List<Wave> waves)
    {
        _moveOnEnemy = new UnityEvent();
        _waves = waves.ToList();
        _prefab = prefab;
        _spawnerTile = spawnerTile;
    }

    public void UpdateSpawner()
    {
        if (_indexWave > _waves.Count)
            return;
        
        if (_timeToWave <= 0 && _indexWave < _waves.Count)
        {
            var wave = _waves[_indexWave++];
            _currentWave = wave.WaveEnemy;
            _countEnemiesWave = _currentWave.Count;
            _timeToWave = wave.RecoverTimeWave;
            _indexEnemy = 0;
        }

        if (_countEnemiesWave <= 0)
            _timeToWave -= Time.deltaTime;
        else if (_timeToEnemy <= 0)
        {
            var enemyBehaviour = _currentWave[_currentWave.Count - _countEnemiesWave--].GetBehaviour();
            _timeToEnemy = _waves[_indexWave - 1].RecoverTimeEnemies;
            var enemy = Object.Instantiate(_prefab);
            enemy.Initialize(enemyBehaviour,_spawnerTile);
            _moveOnEnemy.AddListener(enemy.UpdatePos);
            _enemies.Add(enemy);
        }
        else
            _timeToEnemy -= Time.deltaTime;
        
        if (_moveOnEnemy != null)
            _moveOnEnemy.Invoke();
    }
}
