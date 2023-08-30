
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnerScenario
{
    private EnemySpawner _spawnerTile;
    [SerializeField] private List<Wave> _waves;
    private GameTileFactory _tileFactory;
    private Wave.State _scenario;

    public State Initialize(GameTileFactory tileFactory,EnemySpawner spawnerTile)
    {
        _tileFactory = tileFactory;
        _spawnerTile = spawnerTile;
        return new State(this);
    }
    
    public class State
    {
        private SpawnerScenario _spawnerScenario;
        private Wave.State _wave;
        private int _index;
        public State(SpawnerScenario spawnerScenario)
        {
            _index = 0;
            _spawnerScenario = spawnerScenario;
            _wave = _spawnerScenario._waves[_index++].GetScenario(_spawnerScenario._spawnerTile);
        }

        public bool ScenarioUpdate()
        {
            return _wave.WaveUpdate();
        }

        public void NextWave()
        {
            if (_index < _spawnerScenario._waves.Count)
                _wave = _spawnerScenario._waves[_index++].GetScenario(_spawnerScenario._spawnerTile);
        }
    }
}