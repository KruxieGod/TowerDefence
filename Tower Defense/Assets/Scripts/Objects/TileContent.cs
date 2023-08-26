using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TileContent : MonoBehaviour
{
    [SerializeField] protected TypeOfTile _tileType;
    public TypeOfTile TileType => _tileType;
    public Tile SpawnerTile;
    public virtual bool IsEnded { get; } = true;
    public void InitializeTile(Tile spawnerTile) => SpawnerTile = spawnerTile;
}