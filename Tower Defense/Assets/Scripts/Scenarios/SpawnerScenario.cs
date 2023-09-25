
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SpawnerScenario
{
    [SerializeField] private Vector2Int _positionSpawner;
    [NonSerialized]private EnemySpawner _spawnerTile;
    [SerializeField] private List<Wave> _waves;

    public SpawnerScenarioJson GetJsonClass()
    {
        return new SpawnerScenarioJson(_positionSpawner,_waves.Select(x => x.GetJsonClass()).ToList());
    }
}


[Serializable]
public class SpawnerScenarioJson
{
    [SerializeField] private Vector2Int _positionSpawner;
    [NonSerialized]private EnemySpawner _spawnerTile;
    [SerializeField] private List<WaveJson> _waves;

    public SpawnerScenarioJson(Vector2Int positionSpawner,
        List<WaveJson> waves)
    {
        _positionSpawner = positionSpawner;
        _waves = waves;
    }
    
    public State Initialize(GameBoard gameBoard)
    {
       // _spawnerTile = ProjectContext.Instance.GameProvider.FactoriesProvider.GameFactories.GameTileFactory.GetEnemySpawner().Initialize();
        ISetterTile tile = gameBoard[_positionSpawner.x, _positionSpawner.y];
        tile.SetContentTile(_spawnerTile);
        tile.SetTypeTile(TypeOfTile.SpawnerEnemy);
        return new State(this);
    }
    
    public struct State
    {
        private SpawnerScenarioJson _spawnerScenario;
        private WaveJson.State _wave;
        private int _index;
        public State(SpawnerScenarioJson spawnerScenario)
        {
            _index = 0;
            _spawnerScenario = spawnerScenario;
            if (_spawnerScenario._waves.Count > 0)
                _wave = _spawnerScenario._waves[_index++].GetScenario(_spawnerScenario._spawnerTile);
            else
                _wave = new WaveJson.State();
        }

        public bool ScenarioUpdate()
        {
            return _wave.WaveUpdate();
        }

        public bool NextWave()
        {
            var res = _index < _spawnerScenario._waves.Count;
            if (res)
                _wave = _spawnerScenario._waves[_index++].GetScenario(_spawnerScenario._spawnerTile);
            return res || !_spawnerScenario._spawnerTile.IsEnded;
        }
    }
}