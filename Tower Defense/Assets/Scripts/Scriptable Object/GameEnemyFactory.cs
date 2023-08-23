using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : ScriptableObject
{
    [SerializeField] private Enemy _prefabEnemy;
    private HashSet<EnemySpawner> _dataSpawners = new HashSet<EnemySpawner>();
    public int CountSpawners => _dataSpawners.Count;
    [SerializeField] private LayerMask _layerFloor;
    public LayerMask LayerFloor => _layerFloor;

    public IEnumerable<EnemySpawner> EnemySpawners
    {
        get
        {
            foreach (var spawner in _dataSpawners)
                yield return spawner;
        }
    }

    [SerializeField] private List<Wave> _waves;
    public float SpeedRotation;

    public bool CanRemoveSpawner(Tile tile)
    {
        return tile.Content.IsEnded;
    }

    public void RemoveSpawner(EnemySpawner spawner) => _dataSpawners.Remove(spawner);

    public EnemySpawner GetEnemySpawner(EnemySpawner prefab)
    {
        if (_dataSpawners.TryGetValue(prefab, out var spawner))
            return spawner;
        var enemySpawner = Instantiate(prefab).InitializeWave(_prefabEnemy,_waves,this);
        _dataSpawners.Add(enemySpawner);
        return enemySpawner;
    }

    public void ResetSpawners()
    {
        _dataSpawners.Clear();
    }
}
