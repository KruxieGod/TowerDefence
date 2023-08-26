using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TileContent : MonoBehaviour
{
    [SerializeField] private TypeOfTile _tileType;
    public virtual TypeOfTile TileType => _tileType;
    public Tile SpawnerTile { get; private set; }
    public virtual bool IsEnded { get; } = true;
    public void InitializeTile(Tile spawnerTile) => SpawnerTile = spawnerTile;
}