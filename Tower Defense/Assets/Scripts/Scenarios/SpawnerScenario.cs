
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnerScenario
{
    private Tile _spawnerTile;
    [SerializeField] private List<Wave> _waves;
    private GameTileFactory _tileFactory;
    private Wave.State _scenario;
    private int _index = 0;

    public void Initialize(GameTileFactory tileFactory, Tile spawnerTile)
    {
        _tileFactory = tileFactory;
        _spawnerTile = spawnerTile;
        _scenario = _waves[_index].GetScenario();
    }

    public bool UpdateWave()
    {
        return false;
    }
}