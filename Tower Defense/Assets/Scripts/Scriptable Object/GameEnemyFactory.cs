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
    [SerializeField] private LayerMask _layerFloor;
    public LayerMask LayerFloor => _layerFloor;

    [SerializeField] private List<Wave> _waves;
    public float SpeedRotation;

    public EnemySpawner GetSpawner()
    {
        var enemySpawner = Instantiate(_spawnerPrefab).InitializeWave(_prefabEnemy,_waves,this);
        _data.Add(enemySpawner);
        return enemySpawner;
    }
}
