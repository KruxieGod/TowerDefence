using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        ((ICollectionEntities<EnemySpawner>)GameManager.Spawners).Add(this);
        var enemy = factory.GetPrefabEnemy(enemyInfo);
        _enemies.Add(enemy);
        enemy.transform.position = transform.position;
        enemy.Initialize(SpawnerTile,Remove);
        _moveOnEnemy.AddListener( enemy.UpdatePos);
    }
    
    public void Recycle()
    {
        foreach (var enemy in _enemies)
            if (!enemy.IsUnityNull())
                Destroy(enemy.gameObject);
        _enemies.Clear();
    }

    private void Remove(Enemy enemy)
    {
        _moveOnEnemy.RemoveListener(enemy.UpdatePos);
        _enemies.Remove(enemy);
    }

    private void OnDestroy() => ((ICollectionEntities<EnemySpawner>)GameManager.Spawners).Remove(this);
}
