using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour,ISetterTile
{
    public TileContent Content { get; private set; }
    public Tile _up, _down, _right, _left,_nextTile;
    public int _distance = int.MaxValue;
    public bool HasPath => _distance != int.MaxValue;
    private static Dictionary<TypeOfTile, Action<Tile>> _actions;

    static Tile()
    {
        _actions = new Dictionary<TypeOfTile, Action<Tile>>()
        {
            { TypeOfTile.Destination, SetPosDestination },
            { TypeOfTile.Empty, (tile) => { } }
        };
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
        tile._distance = 0;
        tile._nextTile = null;
        tile.GetComponentInChildren<MeshRenderer>().enabled = false;
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
        Content = tileContent;
        _actions[Content.TileType](this);
    }

    private Tile SetPath(Tile tile)
    {
        if (tile == null || tile._distance < _distance+1)
            return null;
        tile._nextTile = this;
        tile._distance = _distance+1;
        if (tile._up == this)
            tile.transform.eulerAngles = new Vector3(0, 90, 0);
        else if (tile._down == this)
            tile.transform.eulerAngles = new Vector3(0, 270, 0);
        else if (tile._right == this)
            tile.transform.eulerAngles = new Vector3(0, 180, 0);
        else
            tile.transform.eulerAngles = new Vector3(0, 0, 0);
        return tile;
    }
}
