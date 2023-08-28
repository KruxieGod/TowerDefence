using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : BaseFactoryCollection<EnemySpawner>
{
    [SerializeField] private EnemySpawner _spawnerPrefab;
    [SerializeField] private Enemy _prefabEnemy;
    public int CountSpawners => _data.Count;
    [SerializeField] private List<Wave> _waves;

    public EnemySpawner GetSpawner()
    {
        var enemySpawner = Instantiate(_spawnerPrefab).InitializeWave(_prefabEnemy,_waves);
        _data.Add(enemySpawner);
        return enemySpawner;
    }
}
