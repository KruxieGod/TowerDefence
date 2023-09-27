using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Jobs;
using Zenject;
using Input = UnityEngine.Input;

[BurstCompile]
public struct RotationJob : IJobParallelForTransform
{
    public NativeArray<Vector3> ForwardEnemies;
    public NativeArray<Direction> CurrentTileDirections;
    public NativeArray<Direction> PreviousTileDirections;
    public NativeArray<float> Speed;
    public float DeltaTime;
    public void Execute(int index,TransformAccess transform)
    {
        var speedRotation = Enemy.SpeedRotation;
        transform.rotation =  Quaternion.Lerp( transform.rotation,CurrentTileDirections[index].GetDirection(),speedRotation*DeltaTime*Speed[index]);
        transform.position += ForwardEnemies[index] * Speed[index] * DeltaTime;
    }
}

public class GameManager : MonoBehaviour
{
    [Obsolete("Obsolete")]
    private void ParallelEnemies()
    {
        var length = Enemies.Count;
        var enemies= Enemies.Data.ToArray();
        NativeArray<RaycastCommand> commands = new(length,Allocator.TempJob);
        var layer = ProjectContexter.Instance.LayerFloor;
        for (int i = 0; i < length; i++)
        {
            var enemy = enemies[i];
            commands[i] = new RaycastCommand(
                enemy.transform.position + Vector3.up,
                Vector3.down,
                Enemy.Distance,
                layer);
        }
        NativeArray<RaycastHit> hits = new(length,Allocator.TempJob);
        var jobRaycast = RaycastCommand.ScheduleBatch(commands, hits,1);
        jobRaycast.Complete();
        for (int i = 0; i < length; i++)
            enemies[i].TryGetComponentTile(hits[i]);
        commands.Dispose();
        hits.Dispose();
    }
    
    [SerializeField] private bool _isJob = true;
    [field: SerializeField] public int DistanceFromCamera { get; private set; }
    public static UnityEvent OnDestroy { get; set; } = new UnityEvent();
    [Inject]
    private PassedCounter _counter;
    public static GameManager Instance;
    private GameTileFactory _factory;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Vector2Int _size;
    [field :SerializeField] public Camera Camera { get; private set; }
    private GameTowerFactory _towerFactory;
    private GameScenarioJson _scenario;
    public static CollectionEntities<Enemy> Enemies { get; private set; } = new();
    public static CollectionEntities<EnemySpawner>
        Spawners { get; private set; } = new ();
    private GameScenarioJson.State _currentScenario;
    private bool _isPaused;
    private bool _isEndedGame;
    public static  Ray _ray =>Instance.Camera.ScreenPointToRay(Input.mousePosition);
    public LayerMask FloorLayer => _gameBoard.LayerFloor;
    [Inject] private TilesCounter _tilesCounter;
    [Inject] private CounterMoney _counterMoney;
    void Start()
    {
        Instance = this;
        _counter.SetEvent();
        StartNewGame();
    }

    [Inject]
    private void Construct(GameTowerFactory towerFactory,
        GameScenarioJson scenario,
        GameTileFactory factory)
    {
        _towerFactory = towerFactory;
        _scenario = scenario;
        _factory = factory;
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

        if (_isJob)
        {
            ParallelEnemies();
        }
        else
        {
            Debug.Log("No Jobs");
            foreach (var spawner in Spawners.Data)
                spawner.UpdateEntity();
        }
        
        _currentScenario.ScenarioUpdate();
        OnDestroy?.Invoke();
    }
    
    public void StartNewGame()
    {
        foreach (var spawner in Spawners.Data)
            spawner.Recycle();
        _tilesCounter.Reset();
        _counterMoney.Reset();
        _isEndedGame = false;
        _isPaused = false;
        _gameBoard.Initialize(_size,_factory);
        _currentScenario = _scenario.GetScenario(_gameBoard);
        _counter.Initialize();
        _gameBoard.SetPathwayToDestinations();
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
        if (Spawners.Count != 0 &&
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
            _tilesCounter.Replace(tile.Content.TileType);
        }
        else if (!_tilesCounter.TryPlace(typeOf))
        {
            Destroy(content.gameObject);
            return false;
        }
        
        if (typeOf != TypeOfTile.Empty && typeOf != tile.Content.TileType && tile.Content.TileType != TypeOfTile.Empty)
            _tilesCounter.Replace(tile.Content.TileType);
        content.enabled = true;
        _gameBoard.SetType(tile,typeOf);
        _gameBoard.SetContent(tile, typeOf == TypeOfTile.Empty ? _factory.GetContent(TypeOfTile.Empty) : content);
        return true;
    }
}
