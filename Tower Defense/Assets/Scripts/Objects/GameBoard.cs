using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Serialization;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _plane;
    [SerializeField] private Tile _prefabTile;
    public Vector2Int Size { get; private set; }
    private Tile[,] _board;
    public const int POSITIONMULTIPLIER = 10;
    private GameTileFactory _factory;
    private HashSet<ISetterTile> _destinations = new HashSet<ISetterTile>();
    private Func<Tile, Tile, bool> _checkOnType;
    private GameEnemyFactory _enemyFactory;
    public int CountDestinations => _destinations.Count;

    public void Initialize(Vector2Int size,GameTileFactory factory,GameEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        _factory = factory;
        Size = size;
        _plane.localScale = new Vector3(Size.x, 1, Size.y);
        _board = new Tile[Size.x, Size.y];
        FillBoard();
        AddDestination(_board[(Size.x-1)/2,(Size.y-1)/2]);
    }

    private void FillBoard()
    {
        var xSize = _board.GetLength(0);
        var ySize = _board.GetLength(1);
        Vector2Int offcetSize = new Vector2Int(xSize/2,ySize/2);
        
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Tile tile = _board[x,y] = Instantiate(_prefabTile);
                tile.transform.position = new Vector3(POSITIONMULTIPLIER*(x-offcetSize.x),0.1f,POSITIONMULTIPLIER*(y - offcetSize.y));
                tile.transform.parent = this.transform;
                SetType(tile, TypeOfTile.Empty);
                if (x > 0)
                    tile.SetRightToLeft(_board[x-1,y]);
                if (y > 0)
                    tile.SetUpToDown(_board[x,y-1]);
            }
        }
    }

    public void PathUpdate()
    {
        if (_destinations.Count == 0)
            throw new ArgumentException("No one destination Exception");
        ResetAllPaths();
        foreach (var destination in _destinations)
            SetCorrectDirectionTo(destination);
            
    }

    public void AddDestination(ISetterTile tileDestination)
    {
        if (_destinations.Contains(tileDestination))
            return;
        _destinations.Add(tileDestination);
        SetCorrectDirectionTo(tileDestination);
    }
    
    private void SetCorrectDirectionTo(ISetterTile tileDestination)
    {
        SetType(tileDestination,TypeOfTile.Destination);
        Queue<ISetterTile> queue = new Queue<ISetterTile>();
        queue.Enqueue(tileDestination);
        while (queue.Count > 0)
        {
            ISetterTile tile = queue.Dequeue();
            if (tile == null)
                continue;
            queue.Enqueue(tile.SetRightTile());
            queue.Enqueue(tile.SetLeftTile());
            queue.Enqueue(tile.SetDownTile());
            queue.Enqueue(tile.SetUpTile());
        }
    }

    private void ResetAllPaths()
    {
        foreach (var tile in _board)
            tile.ResetDistance();
    }

    public void SetType(ISetterTile tileDestination,TypeOfTile type)
    {
        if (type != TypeOfTile.Destination)
            _destinations.Remove(tileDestination);
        else
            _destinations.Add(tileDestination);
        if (type == TypeOfTile.SpawnerEnemy)
            _enemyFactory.GetEnemySpawner((Tile)tileDestination);
        var typeTile = _factory.GetContent(type);
        typeTile.transform.parent = transform;
        tileDestination.SetTypeTile(typeTile);
    }

    public Tile GetTile(Ray ray)
    {
        if (Physics.Raycast(ray, out var hit))
        {
            Debug.Log(hit.point);
            int x = (int)(hit.point.x/POSITIONMULTIPLIER + Size.x * 0.5f);
            int y = (int)(hit.point.z/POSITIONMULTIPLIER + Size.y * 0.5f);
            Debug.Log((x,y).ToString());
            if (x >= 0 && x < Size.x && y >= 0 && y < Size.y)
                return _board[x, y];
        }

        return null;
    }
}
