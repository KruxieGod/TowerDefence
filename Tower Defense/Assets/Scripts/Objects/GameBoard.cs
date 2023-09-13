using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _plane;
    [SerializeField] private Tile _prefabTile;
    public Vector2Int Size { get; private set; }
    private Tile[,] _board;
    public const int POSITIONMULTIPLIER = 10;
    private GameTileFactory _factory;
    private HashSet<ISetterTile> _destinations = new HashSet<ISetterTile>();
    private Func<Tile, Tile, bool> _checkOnType;
    public int CountDestinations => _destinations.Count;

    public void Initialize(Vector2Int size,GameTileFactory factory)
    {
        _factory = factory;
        Size = size;
        if (!_plane.IsUnityNull() || _plane != null)
            _plane.localScale = new Vector3(Size.x, 1, Size.y);
        if (_board != null)
            Clear();
        else
        {
            _board = new Tile[Size.x, Size.y];
            FillBoard();
        }
        AddDestination(_board[(Size.x-1)/2,(Size.y-1)/2]);
    }

    private void Clear()
    {
        var lengthI = _board.GetLength(0);
        var lengthJ = _board.GetLength(1);
        for (int i = 0; i < lengthI; i++)
            for (int j = 0; j < lengthJ; j++)
            {
                var tile = _board[i, j];
                SetType(tile, TypeOfTile.Empty);
                SetContent(tile,_factory.GetContent(TypeOfTile.Empty));
            }
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
                SetContent(tile,_factory.GetContent(TypeOfTile.Empty));
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
            SetCorrectDirectionTo(destination,(original, next) => original.Distance+1 > next.Distance);
    }

    public void AddDestination(ISetterTile tileDestination)
    {
        if (_destinations.Contains(tileDestination))
            return;
        ResetAllPaths();
        _destinations.Add(tileDestination);
        SetCorrectDirectionTo(tileDestination, (original,next) => next.IsHasPath);
    }
    
    private void SetCorrectDirectionTo(ISetterTile tileDestination,Func<Tile,Tile,bool> checkOnPath)
    {
        SetType(tileDestination,TypeOfTile.Destination);
        SetContent(tileDestination,_factory.GetContent(TypeOfTile.Destination));
        Queue<ISetterTile> queue = new Queue<ISetterTile>();
        queue.Enqueue(tileDestination);
        while (queue.Count > 0)
        {
            ISetterTile tile = queue.Dequeue();
            if (tile == null)
                continue;
            queue.Enqueue(tile.SetRightTile(checkOnPath));
            queue.Enqueue(tile.SetLeftTile(checkOnPath));
            queue.Enqueue(tile.SetDownTile(checkOnPath));
            queue.Enqueue(tile.SetUpTile(checkOnPath));
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
        tileDestination.SetTypeTile(type);
    }

    public void SetContent(ISetterTile tileDestination,TileContent tileContent)
    {
        tileContent.transform.parent = transform;
        tileDestination.SetContentTile(tileContent);
    }

    public Tile GetTile(Ray ray)
    {
        if (Physics.Raycast(ray, out var hit, 2000,_layer))
        {
            Debug.Log("Tile is gave");
            int x = (int)(hit.point.x/POSITIONMULTIPLIER + Size.x * 0.5f);
            int y = (int)(hit.point.z/POSITIONMULTIPLIER + Size.y * 0.5f);
            if (x >= 0 && x < Size.x && y >= 0 && y < Size.y)
                return _board[x, y];
        }

        return null;
    }

    public Tile this[int i, int j]
    {
        get
        {
            var lengthI = _board.GetLength(0);
            var lengthJ = _board.GetLength(1);
            if (i < lengthI && i >= 0 && j < lengthJ && j >= 0)
                return _board[i, j];
            throw new ArgumentOutOfRangeException();
        }
    }
}
