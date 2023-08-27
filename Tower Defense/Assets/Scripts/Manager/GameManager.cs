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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetTileOnPath(TypeOfTile.Destination,
                () => _factory.GetContent(TypeOfTile.Destination));
        else if (Input.GetMouseButtonDown(1))
            SetTileOnPath(TypeOfTile.Wall,
                () => _factory.GetContent(TypeOfTile.Wall));
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            SetTileOnPath(TypeOfTile.SpawnerEnemy,
                _enemyFactory.GetSpawner);
        else if(Input.GetKeyDown(KeyCode.E))
            SetTileOnPath(TypeOfTile.Turret,
                _towerFactory.GetBallista);
        else if(Input.GetKeyDown(KeyCode.R))
            SetTileOnPath(TypeOfTile.Turret,
                _towerFactory.GetLaserTurret);
        
        foreach (var tower in _towerFactory.Data)
            tower?.UpdateEntity();
        
        foreach (var spawner in _enemyFactory.Data)
            spawner?.UpdateSpawner();
    }

    private void SetTileOnPath(TypeOfTile type,Func<TileContent> content)
    {
        if (!TrySetTile(type,content)) 
            return;
        _gameBoard.PathUpdate();
        if (_enemyFactory.CountSpawners != 0 &&
            !_enemyFactory.Data.All(x => x.SpawnerTile.HasPath()))
        {
            TrySetTile(TypeOfTile.Empty,content);
            _gameBoard.PathUpdate();
        }
    }
    
    private bool TrySetTile(TypeOfTile type,Func<TileContent> content)
    {
        var tile = _gameBoard.GetTile(_ray);
        if (tile == null ||
            Physics.OverlapBox(tile.transform.position,new Vector3(Enemy.RADIUS,Enemy.RADIUS,Enemy.RADIUS))
                .Any(x => x.CompareTag("Enemy")) || 
            !tile.CanBeSet(_gameBoard,_enemyFactory))
            return false;
        var typeOf = type == tile.Content.TileType ? TypeOfTile.Empty : type;
        _gameBoard.SetType(tile,typeOf);
        _gameBoard.SetContent(tile, typeOf == TypeOfTile.Empty ? _factory.GetContent(TypeOfTile.Empty) : content());
        return true;
    }
}
