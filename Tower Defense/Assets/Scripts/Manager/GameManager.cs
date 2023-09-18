using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Jobs;
using Input = UnityEngine.Input;

public struct TransformInfo
{
    public readonly Quaternion Rotation;
    public readonly Vector3 Position;

    public TransformInfo( Quaternion rotation, Vector3 position)
    {
        Rotation = rotation;
        Position = position;
    }
}

public class GameManager : MonoBehaviour
{
    private UniTask<TransformInfo[]> ParallelEnemies(IEnumerable<Enemy> enemies)
    {
        transform.position += Vector3.left;
        foreach (var enemy in enemies)
        {
            var enemyTransform = enemy.transform;
            var pos = enemyTransform.position;
            var forward = enemyTransform.forward;
            var rot = enemyTransform.rotation;
            _tasks.Add(UniTask.RunOnThreadPool(() => enemy.GetInfos(pos,forward,rot),false));
        }
        return UniTask.WhenAll(_tasks);
    }

    private List<UniTask<TransformInfo>> _tasks = new();
    [SerializeField] private bool _isJob = true;
    [field: SerializeField] public int DistanceFromCamera { get; private set; }
    public static UnityEvent OnDestroy { get; set; } = new UnityEvent();
    [SerializeField] private PassedCounter _counter;
    private GameTileFactory _factory => ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTileFactory;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Vector2Int _size;
    [field :SerializeField] public Camera Camera { get; private set; }
    private GameTowerFactory _towerFactory => ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTowerFactory;
    private GameScenarioJson _scenario => ProjectContext.Instance.GameProvider.ScenariosProvider.GetCurrentScenario();
    public static CollectionEntities<Enemy> Enemies { get; private set; } = new();
    public static CollectionEntities<EnemySpawner>
        Spawners { get; private set; } = new ();
    private GameScenarioJson.State _currentScenario;
    private bool _isPaused;
    private bool _isEndedGame;
    public static  Ray _ray =>ProjectContext.Instance.GameObjectsProvider.GameManager.Camera.ScreenPointToRay(Input.mousePosition);
    void Start()
    {
        _counter.SetEvent();
        StartNewGame();
    }

    private async void Update()
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
            var enemies = Enemies.Data.ToList();
            var array = await ParallelEnemies(enemies);
            for (int i = 0; i < enemies.Count; i++)
            {
                var info = array[i];
                enemies[i].transform.position = info.Position;
                enemies[i].transform.rotation = info.Rotation;
            }
            _tasks.Clear();
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
        ProjectContext.Instance.GameSceneLoader.TilesCounterLoader?.TilesCounter?.Reset();
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
            ProjectContext.Instance.GameSceneLoader.TilesCounterLoader.TilesCounter.Replace(tile.Content.TileType);
        }
        else if (!ProjectContext.Instance.GameSceneLoader.TilesCounterLoader.TilesCounter.TryPlace(typeOf))
        {
            Destroy(content.gameObject);
            return false;
        }

        if (typeOf != TypeOfTile.Empty && typeOf != tile.Content.TileType && tile.Content.TileType != TypeOfTile.Empty)
            ProjectContext.Instance.GameSceneLoader.TilesCounterLoader.TilesCounter.Replace(tile.Content.TileType);
        content.enabled = true;
        _gameBoard.SetType(tile,typeOf);
        _gameBoard.SetContent(tile, typeOf == TypeOfTile.Empty ? _factory.GetContent(TypeOfTile.Empty) : content);
        return true;
    }
}
