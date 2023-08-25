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
    [SerializeField] private GameTowerFactory _towerFactory;
    
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);
    void Start()
    {
        _gameBoard.Initialize(_size,_factory,_enemyFactory);
        _factory.Initialize(_enemyFactory,_towerFactory);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetTileOnPath(TypeOfTile.Destination);
        else if (Input.GetMouseButtonDown(1))
            SetTileOnPath(TypeOfTile.Wall);
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            SetTileOnPath(TypeOfTile.SpawnerEnemy);
        else if(Input.GetKeyDown(KeyCode.T))
            SetTileOnPath(TypeOfTile.Tourrel);
        
        foreach (var spawner in _enemyFactory.Data)
            spawner.UpdateSpawner();
        
        foreach (var tower in _towerFactory.Data)
            tower.TowerUpdate();
    }

    private void SetTileOnPath(TypeOfTile type)
    {
        if (!TrySetTile(type)) 
            return;
        _gameBoard.PathUpdate();
        if (_enemyFactory.CountSpawners != 0 &&
            !_enemyFactory.Data.All(x => x.SpawnerTile.HasPath()))
        {
            TrySetTile(TypeOfTile.Empty);
            _gameBoard.PathUpdate();
        }
    }
    
    private bool TrySetTile(TypeOfTile type)
    {
        var tile = _gameBoard.GetTile(_ray);
        if (tile == null ||
            Physics.OverlapBox(tile.transform.position,new Vector3(Enemy.RADIUS,Enemy.RADIUS,Enemy.RADIUS))
                .Any(x => x.CompareTag("Enemy")) || 
            !tile.CanBeSet(_gameBoard,_enemyFactory))
            return false;
        _gameBoard.SetType(tile,type == tile.Content.TileType ? TypeOfTile.Empty : type);
        return true;
    }
}
