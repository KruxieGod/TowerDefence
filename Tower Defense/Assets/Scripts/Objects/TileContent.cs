using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContent : MonoBehaviour
{
    [SerializeField] private TypeOfTile _tileType;
    public TypeOfTile TileType => _tileType;
}

public enum TypeOfTile
{
    Empty,
    Destination,
    Wall
}
