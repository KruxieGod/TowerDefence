using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : BaseFactoryCollection<EnemySpawner>
{
    [SerializeField] private Enemy _prefabEnemy;
    public int CountSpawners => _data.Count;
    [SerializeField] private LayerMask _layerFloor;
    public LayerMask LayerFloor => _layerFloor;

    [SerializeField] private List<Wave> _waves;
    public float SpeedRotation;
    
    protected override EnemySpawner Initialization(EnemySpawner prefab)
    {
        return prefab.InitializeWave(_prefabEnemy,_waves,this);
    }
    
    public bool CanRemoveSpawner(Tile tile)
    {
        return tile.Content.IsEnded;
    }
    
    public void RemoveSpawner(EnemySpawner spawner) => _data.Remove(spawner);

    public void ResetSpawners()
    {
        _data.Clear();
    }
}
