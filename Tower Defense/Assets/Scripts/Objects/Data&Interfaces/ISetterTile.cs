using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetterTile
{
    Tile SetUpTile(Func<Tile,Tile,bool> checkOnPath);
    Tile SetDownTile(Func<Tile,Tile,bool> checkOnPath);
    Tile SetRightTile(Func<Tile,Tile,bool> checkOnPath);
    Tile SetLeftTile(Func<Tile,Tile,bool> checkOnPath);
    void SetTypeTile(TypeOfTile tileContent);
    void SetContentTile(TileContent tileContent);
}
