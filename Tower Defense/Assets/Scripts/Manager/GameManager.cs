using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnDestroy { get; set; } = new UnityEvent();
    [SerializeField] private PassedCounter _counter;
    private GameTileFactory _factory => ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTileFactory;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Camera _camera;
    private GameTowerFactory _towerFactory => ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTowerFactory;
    private GameScenarioJson _scenario => ProjectContext.Instance.GameProvider.ScenariosProvider.GetCurrentScenario();

    public static CollectionEntities<EnemySpawner>
        Spawners { get; private set; } = new CollectionEntities<EnemySpawner>();
    private GameScenarioJson.State _currentScenario;
    private bool _isPaused;
    private Action _onReset;
    private bool _isEndedGame;
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);
    void Start()
    {
        _counter.SetEvent();
        StartNewGame();
    }

    private void Update()
    {
        if (_isEndedGame)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
        if (_isPaused)
            return;
        if (Input.GetMouseButtonDown(0))
            SetTileOnPath(TypeOfTile.Destination,
                () => _factory.GetContent(TypeOfTile.Destination));
        else if (Input.GetMouseButtonDown(1))
            SetTileOnPath(TypeOfTile.Wall,
                () => _factory.GetContent(TypeOfTile.Wall));
        else if(Input.GetKeyDown(KeyCode.E))
            SetTileOnPath(TypeOfTile.Turret,
                _towerFactory.GetBallista);
        else if(Input.GetKeyDown(KeyCode.R))
            SetTileOnPath(TypeOfTile.Turret,
                _towerFactory.GetLaserTurret);
        
        foreach (var tower in _towerFactory.Data)
            tower?.UpdateEntity();
        
        foreach (var spawner in Spawners.Data)
            spawner?.UpdateEntity();
        _currentScenario.ScenarioUpdate();
        OnDestroy?.Invoke();
        _onReset?.Invoke();
    }

    public void ResetGame()
    {
        _onReset += StartNewGame;
    }

    private void StartNewGame()
    {
        foreach (var spawner in Spawners.Data)
            spawner.Recycle();
        _gameBoard.Initialize(_size,_factory);
        _currentScenario = _scenario.GetScenario(_gameBoard);
        _counter.Initialize(this);
        _onReset -= StartNewGame;
        Debug.Log("GG");
    }

    public void EndGame()
    {
        _isEndedGame = true;
        Pause(true);
    }

    private void Pause(bool pause = false)
    {
        _isPaused = !_isPaused || pause;
    }
    
    private void SetTileOnPath(TypeOfTile type,Func<TileContent> content)
    {
        if (!TrySetTile(type,content)) 
            return;
        _gameBoard.PathUpdate();
        if (Spawners.CountSpawners != 0 &&
            !Spawners.Data.All(x => x.SpawnerTile.HasPath()))
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
            !tile.CanBeSet(_gameBoard))
            return false;
        var typeOf = type == tile.Content.TileType ? TypeOfTile.Empty : type;
        _gameBoard.SetType(tile,typeOf);
        _gameBoard.SetContent(tile, typeOf == TypeOfTile.Empty ? _factory.GetContent(TypeOfTile.Empty) : content());
        return true;
    }
}
