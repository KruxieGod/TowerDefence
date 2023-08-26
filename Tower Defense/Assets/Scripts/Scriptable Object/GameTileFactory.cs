using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class GameTileFactory : ScriptableObject
{
    [SerializeField] private TileContent _destinationPrefab;
    [SerializeField] private TileContent _emptyPrefab;
    [SerializeField] private TileContent _wallPrefab;
    
    private Dictionary<TypeOfTile, TileContent> _prefabs;

    private void OnEnable()
    {
        _prefabs = new()
        {
            { TypeOfTile.Destination, _destinationPrefab},
            { TypeOfTile.Empty, _emptyPrefab },
            { TypeOfTile.Wall, _wallPrefab }
        };
    }

    public TileContent GetContent(TypeOfTile typeOfTile)
    {
        return Instantiate(_prefabs[typeOfTile]);
    }
}
