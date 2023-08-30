using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class EnemySpawner : TileContent, IUpdatable
{
    public override TypeOfTile TileType => TypeOfTile.SpawnerEnemy;
    private HashSet<Enemy> _enemies = new HashSet<Enemy>();
    private UnityEvent _moveOnEnemy = new UnityEvent();
    
    public void UpdateEntity()
    {
        _moveOnEnemy.Invoke();
    }

    public void SpawnEnemy(EnemyInfo enemyInfo, GameEnemyFactory factory)
    {
        var enemy = factory.GetPrefabEnemy(enemyInfo);
        _enemies.Add(enemy);
        enemy.transform.position = transform.position;
        enemy.Initialize(SpawnerTile,Remove);
        _moveOnEnemy.AddListener( enemy.UpdatePos);
    }
    
    private void Remove(Enemy enemy) => _enemies.Remove(enemy);
}
