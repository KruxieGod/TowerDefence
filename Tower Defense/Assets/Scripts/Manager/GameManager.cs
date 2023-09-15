using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public int DistanceFromCamera { get; private set; }
    public static UnityEvent OnDestroy { get; set; } = new UnityEvent();
    [SerializeField] private PassedCounter _counter;
    private GameTileFactory _factory => ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTileFactory;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Vector2Int _size;
    [field :SerializeField] public Camera Camera { get; private set; }
    private GameTowerFactory _towerFactory => ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTowerFactory;
    private GameScenarioJson _scenario => ProjectContext.Instance.GameProvider.ScenariosProvider.GetCurrentScenario();

    public static CollectionEntities<EnemySpawner>
        Spawners { get; private set; } = new CollectionEntities<EnemySpawner>();
    private GameScenarioJson.State _currentScenario;
    private bool _isPaused;
    private bool _isEndedGame;
    public static  Ray _ray =>ProjectContext.Instance.GameObjectsProvider.GameManager.Camera.ScreenPointToRay(Input.mousePosition);
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
        if (Input.GetMouseButtonDown(0) &&
            Physics.Raycast(_ray,out var hit) && 
            hit.transform.TryGetComponent<IPointerDownHandler>(out var component))
            component.OnPointerDown(null);
        
        foreach (var tower in _towerFactory.Data)
            tower?.UpdateEntity();
        
        foreach (var spawner in Spawners.Data)
            spawner?.UpdateEntity();
        _currentScenario.ScenarioUpdate();
        OnDestroy?.Invoke();
    }

    public void StartNewGame()
    {
        foreach (var spawner in Spawners.Data)
            spawner.Recycle();
        ProjectContext.Instance.GameSceneLoader.TilesCounterUILoader?.TilesCounter?.Reset();
        _isEndedGame = false;
        _isPaused = false;
        _gameBoard.Initialize(_size,_factory);
        _currentScenario = _scenario.GetScenario(_gameBoard);
        _counter.Initialize(this);
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
    
    public void SetTileOnPath(TypeOfTile type,TileContent content)
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
    
    private bool TrySetTile(TypeOfTile type,TileContent content)
    {
        var tile = _gameBoard.GetTile(_ray);
        if (tile == null ||
            Physics.OverlapBox(tile.transform.position, new Vector3(Enemy.RADIUS, Enemy.RADIUS, Enemy.RADIUS))
                .Any(x => x.CompareTag("Enemy")) ||
            !tile.CanBeSet(_gameBoard))
        {
            Destroy(content.gameObject);
            return false;
        }
        var typeOf = type == tile.Content.TileType ? TypeOfTile.Empty : type;
        if (typeOf == TypeOfTile.Empty)
        {
            Destroy(content.gameObject);
            ProjectContext.Instance.GameSceneLoader.TilesCounterUILoader.TilesCounter.Replace(tile.Content.TileType);
        }
        else if (!ProjectContext.Instance.GameSceneLoader.TilesCounterUILoader.TilesCounter.TryPlace(typeOf))
        {
            Destroy(content.gameObject);
            return false;
        }

        if (typeOf != TypeOfTile.Empty && typeOf != tile.Content.TileType && tile.Content.TileType != TypeOfTile.Empty)
            ProjectContext.Instance.GameSceneLoader.TilesCounterUILoader.TilesCounter.Replace(tile.Content.TileType);
        content.enabled = true;
        _gameBoard.SetType(tile,typeOf);
        _gameBoard.SetContent(tile, typeOf == TypeOfTile.Empty ? _factory.GetContent(TypeOfTile.Empty) : content);
        return true;
    }
}
