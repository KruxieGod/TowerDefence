using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : ScriptableObject
{
    [SerializeField] private Enemy _prefabEnemy;
    public List<EnemySpawner> EnemySpawners { get; private set; } = new List<EnemySpawner>();
    [SerializeField] private List<Wave> _waves;
    public float SpeedRotation;

    public EnemySpawner GetEnemySpawner(Tile enemySpawn)
    {
        var enemySpawner = new EnemySpawner(enemySpawn,_prefabEnemy,_waves,this);
        EnemySpawners.Add(enemySpawner);
        return enemySpawner;
    }
}
