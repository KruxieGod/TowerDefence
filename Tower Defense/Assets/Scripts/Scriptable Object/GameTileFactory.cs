using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTileFactory : ScriptableObject
{
    [SerializeField] private TileContent _destinationPrefab;
    [SerializeField] private TileContent _emptyPrefab;

    private Dictionary<TypeOfTile, TileContent> _prefabs = new Dictionary<TypeOfTile, TileContent>();

    private void OnEnable()
    {
        _prefabs.Add(TypeOfTile.Destination,_destinationPrefab);
        _prefabs.Add(TypeOfTile.Empty,_emptyPrefab);
    }

    public TileContent GetContent(TypeOfTile typeOfTile)
    {
        return Instantiate(_prefabs[typeOfTile]);
    }
}
