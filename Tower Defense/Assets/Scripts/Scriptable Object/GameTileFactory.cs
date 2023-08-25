using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTileFactory : ScriptableObject
{
    [SerializeField] private TileContent _destinationPrefab;
    [SerializeField] private TileContent _emptyPrefab;
    [SerializeField] private TileContent _wallPrefab;
    [SerializeField] private EnemySpawner _spawnerEnemyPrefab;
    [SerializeField] private Tourrel _tourrelPrefab;
    
    private GameEnemyFactory _enemyFactory;
    private GameTowerFactory _towerFactory;
    
    private Dictionary<TypeOfTile, Func<TileContent>> _prefabs;

    public void Initialize(GameEnemyFactory enemyFactory, GameTowerFactory towerFactory)
    {
        _enemyFactory = enemyFactory;
        _towerFactory = towerFactory;
    }

    private void OnEnable()
    {
        _prefabs = new()
        {
            { TypeOfTile.Destination, () => Instantiate(_destinationPrefab) },
            { TypeOfTile.Empty, () => Instantiate(_emptyPrefab) },
            { TypeOfTile.Wall, () => Instantiate(_wallPrefab) },
            { TypeOfTile.SpawnerEnemy, () => _enemyFactory.GetPrefab(_spawnerEnemyPrefab) },
            { TypeOfTile.Tourrel, () => _towerFactory.GetPrefab(_tourrelPrefab) }
        };
    }

    public TileContent GetContent(TypeOfTile typeOfTile)
    {
        return _prefabs[typeOfTile]?.Invoke();
    }
}
