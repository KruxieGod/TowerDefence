using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class GameTileFactory : ScriptableObject
{
    [SerializeField] private TileContent _destinationPrefab;
    [SerializeField] private TileContent _emptyPrefab;
    [SerializeField] private TileContent _wallPrefab;
    [SerializeField] private EnemySpawner _spawnerPrefab;
    private Dictionary<TypeOfTile, TileContent> _prefabs;

    private void OnEnable()
    {
        _prefabs = new()
        {
            { _destinationPrefab.TileType, _destinationPrefab},
            { _emptyPrefab.TileType, _emptyPrefab },
            { _wallPrefab.TileType, _wallPrefab },
            { _spawnerPrefab.TileType, _spawnerPrefab}
        };
    }

    public TileContent GetContent(TypeOfTile typeOfTile)
    {
        return Instantiate(_prefabs[typeOfTile]);
    }

    public EnemySpawner GetEnemySpawner()
    {
        var enemySpawner = Instantiate(_spawnerPrefab);
        return enemySpawner;
    }
}
