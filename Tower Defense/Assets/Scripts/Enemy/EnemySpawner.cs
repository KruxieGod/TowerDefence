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
    private HashSet<Enemy> _enemies = new ();
    private UnityEvent _moveOnEnemy = new ();
    public override bool IsEnded => _enemies.Count == 0;
    
    public void UpdateEntity()
    {
        _moveOnEnemy.Invoke();
    }

    public void SpawnEnemy(EnemyInfo enemyInfo, GameEnemyFactory factory)
    {
        var enemy = factory.GetPrefabEnemy(enemyInfo);
        GameManager.Enemies.Add(enemy);
        
        _enemies.Add(enemy);
        enemy.transform.position = transform.position;
        enemy.Initialize(SpawnerTile,Remove);
        _moveOnEnemy.AddListener( enemy.UpdatePos);
    }

    public EnemySpawner Initialize()
    {
        ((ICollectionEntities<EnemySpawner>)GameManager.Spawners).Add(this);
        return this;
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
        GameManager.Enemies.Remove(enemy);
        _moveOnEnemy.RemoveListener(enemy.UpdatePos);
        _enemies.Remove(enemy);
    }

    private void OnDestroy() => ((ICollectionEntities<EnemySpawner>)GameManager.Spawners).Remove(this);
}
