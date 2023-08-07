using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour,ISetterTile
{
    [SerializeField] private GameObject _arrow;
    public TileContent Content { get; private set; }
    public Tile _up, _down, _right, _left,_nextTile;
    public Tile NextTile => _nextTile;
    private int _distance = int.MaxValue;
    public bool IsHasPath => _distance != int.MaxValue;
    private bool _isWall;
    public Direction Direction { get; private set; }
    private static Dictionary<TypeOfTile, Action<Tile>> _actions;
    private static Func<Tile, Tile, bool> _checkOn;
    static Tile()
    {
        _actions = new Dictionary<TypeOfTile, Action<Tile>>()
        {
            { TypeOfTile.Destination, tile =>
            {
                _checkOn = (original, next) => original._distance+1 > next._distance;
                SetPosDestination(tile);
            }},
            { TypeOfTile.Empty, tile =>
                {
                    tile._isWall = false;
                    tile._arrow.SetActive(true);
                }
            },
            { TypeOfTile.Wall , tile =>
                {
                    _checkOn = (original, next) => next.IsHasPath;
                    tile._nextTile = null;
                    tile._distance = int.MaxValue;
                    tile._isWall = true;
                }
            },
            { TypeOfTile.SpawnerEnemy , tile =>
            {
                tile._isWall = false;
                tile._arrow.SetActive(true);
            } }
        };
    }

    public void ResetDistance()
    {
        _distance = int.MaxValue;
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

    private static void SetPosDestination(Tile tile)
    {
        tile._isWall = false;
        tile._distance = 0;
        tile._nextTile = null;
        tile._arrow.SetActive(false);
    }

    Tile ISetterTile.SetUpTile() => SetPath(_up);

    Tile ISetterTile.SetDownTile() => SetPath(_down);

    Tile ISetterTile.SetRightTile() => SetPath(_right);

    Tile ISetterTile.SetLeftTile() => SetPath(_left);

    void ISetterTile.SetTypeTile(TileContent tileContent)
    {
        if (Content != null)
            Destroy(Content.gameObject);
        tileContent.transform.position = transform.position;
        tileContent.transform.parent = transform;
        Content = tileContent;
        _actions[Content.TileType](this);
    }

    private Tile SetPath(Tile tile)
    {
        if (_isWall|| tile == null|| tile._isWall || _checkOn(this,tile) )
            return null;
        tile._nextTile = this;
        tile._distance = _distance+1;
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
    private static Dictionary<Direction, Quaternion> _directions =
        new Dictionary<Direction, Quaternion>()
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
