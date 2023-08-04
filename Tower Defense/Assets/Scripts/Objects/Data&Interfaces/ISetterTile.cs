using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetterTile
{
    Tile SetUpTile();
    Tile SetDownTile();
    Tile SetRightTile();
    Tile SetLeftTile();
    void SetTypeTile(TileContent tileContent);
}
