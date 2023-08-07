using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : ScriptableObject
{
    [SerializeField] private Enemy _prefabEnemy;
    private Dictionary<Tile, EnemySpawner> _dataSpawners = new Dictionary<Tile, EnemySpawner>();
    public int CountSpawners => _dataSpawners.Count;
    public IEnumerable<EnemySpawner> EnemySpawners
    {
        get
        {
            foreach (var spawner in _dataSpawners)
                yield return spawner.Value;
        }
    }

    [SerializeField] private List<Wave> _waves;
    public float SpeedRotation;

    public EnemySpawner GetEnemySpawner(Tile enemySpawnerTile)
    {
        if (_dataSpawners.ContainsKey(enemySpawnerTile))
            return _dataSpawners[enemySpawnerTile];
        var enemySpawner = new EnemySpawner(enemySpawnerTile,_prefabEnemy,_waves,this);
        _dataSpawners.Add(enemySpawnerTile,enemySpawner);
        return enemySpawner;
    }

    public void RemoveSpawner(Tile spawner)
    {
        _dataSpawners.Remove(spawner);
    }
}
