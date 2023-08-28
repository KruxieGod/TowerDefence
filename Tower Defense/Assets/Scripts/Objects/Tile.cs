using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour,ISetterTile
{
    [SerializeField] private GameObject _arrow;
    public TileContent Content { get; private set; }
    private Tile _up, _down, _right, _left,_nextTile;
    public Tile NextTile => _nextTile;
    public int Distance { get; private set; }
    public bool IsHasPath => Distance != int.MaxValue;
    private bool _isWall;
    public Direction Direction { get; private set; }
    private static Dictionary<TypeOfTile, Action<Tile>> _actions;
    static Tile()
    {
        _actions = new Dictionary<TypeOfTile, Action<Tile>>()
        {
            { TypeOfTile.Destination, SetPosDestination},
            { TypeOfTile.Empty, SetWithoutInteraction},
            { TypeOfTile.Wall , SetWithInteraction},
            { TypeOfTile.SpawnerEnemy ,SetWithoutInteraction},
            { TypeOfTile.Turret , SetWithInteraction}
        };
    }

    #region Interaction With Tile
    private static void SetWithInteraction(Tile tile)
    {
        tile._nextTile = null;
        tile.Distance = int.MaxValue;
        tile._isWall = true;
    }

    private static void SetWithoutInteraction(Tile tile)
    {
        tile._isWall = false;
        tile._arrow.SetActive(true);
    }
    
    private static void SetPosDestination(Tile tile)
    {
        tile._isWall = false;
        tile.Distance = 0;
        tile._nextTile = null;
        tile._arrow.SetActive(false);
    }
    #endregion

    public void ResetDistance()
    {
        Distance = int.MaxValue;
    }
    
    public void SetUpToDown(Tile down)
    {
        down._up = this;
        _down = down;
    }

    public void SetRightToLeft(Tile left)
    {
        left._right = this;
        _left = left;
    }

    Tile ISetterTile.SetUpTile(Func<Tile,Tile,bool> checkOnPath) => SetPath(_up,checkOnPath);

    Tile ISetterTile.SetDownTile(Func<Tile,Tile,bool> checkOnPath) => SetPath(_down,checkOnPath);

    Tile ISetterTile.SetRightTile(Func<Tile,Tile,bool> checkOnPath) => SetPath(_right,checkOnPath);

    Tile ISetterTile.SetLeftTile(Func<Tile,Tile,bool> checkOnPath) => SetPath(_left,checkOnPath);

    void ISetterTile.SetTypeTile(TypeOfTile tileType)
    {
        _actions[tileType](this);
    }
    
    void ISetterTile.SetContentTile(TileContent tileContent)
    {
        if (Content != null)
            GameManager.OnDestroy.AddListener(Content.gameObject.Destroy);
        tileContent.transform.position = transform.position;
        tileContent.transform.parent = transform;
        Content = tileContent;
        Content.InitializeTile(this);
    }

    private Tile SetPath(Tile tile,Func<Tile,Tile,bool> checkOnPath)
    {
        if (_isWall|| tile == null|| tile._isWall || checkOnPath(this,tile) )
            return null;
        tile._nextTile = this;
        tile.Distance = Distance+1;
        if (tile._up == this)
            tile.Direction = Direction.Up;
        else if (tile._down == this)
            tile.Direction = Direction.Down;
        else if (tile._right == this)
            tile.Direction = Direction.Right;
        else
            tile.Direction = Direction.Left;
        tile.transform.rotation = tile.Direction.GetDirection();
        return tile;
    }
}

public static class DirectionExtension
{
    private static readonly Dictionary<Direction, Quaternion> _directions =
        new()
        {
            { Direction.Up, Quaternion.Euler(Vector3.zero) },
            { Direction.Down, Quaternion.Euler(new Vector3(0, 180, 0)) },
            { Direction.Right, Quaternion.Euler(new Vector3(0, 90, 0)) },
            { Direction.Left, Quaternion.Euler(new Vector3(0,270,0)) }
        };
    
    public static Quaternion GetDirection(this Direction type)
    {
        return _directions[type];
    }
}

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}