using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameTileFactory _factory;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameEnemyFactory _enemyFactory;
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);
    void Start()
    {
        _gameBoard.Initialize(_size,_factory,_enemyFactory);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetTile(TypeOfTile.Destination);
        else if (Input.GetMouseButtonDown(1))
            SetTile(TypeOfTile.Wall);
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            SetTile(TypeOfTile.SpawnerEnemy);
        foreach (var spawner in _enemyFactory.EnemySpawners)
            spawner.UpdateSpawner();
    }

    private void SetTile(TypeOfTile type)
    {
        if (!TryGetTile(type)) 
            return;
        _gameBoard.PathUpdate();
        if (_enemyFactory.CountSpawners != 0 && !_enemyFactory.EnemySpawners.All(x => x.SpawnerTile.HasPath()))
        {
            TryGetTile(TypeOfTile.Empty);
            _gameBoard.PathUpdate();
        }
    }
    
    private bool TryGetTile(TypeOfTile type)
    {
        var tile = _gameBoard.GetTile(_ray);
        if (tile != null && tile.Content.TileType == TypeOfTile.Destination && _gameBoard.CountDestinations == 1)
            return false;
        if (tile == null ||
            Physics.OverlapBox(tile.transform.position,new Vector3(Enemy.RADIUS,Enemy.RADIUS,Enemy.RADIUS))
                .Any(x => x.CompareTag("Enemy")))
            return false;
        if (tile.Content.TileType == TypeOfTile.SpawnerEnemy)
            _enemyFactory.RemoveSpawner(tile);
        _gameBoard.SetType(tile,type == tile.Content.TileType ? TypeOfTile.Empty : type);
        return true;
    }
}
