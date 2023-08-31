using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class GameScenario : ScriptableObject
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private List<SpawnerScenario> _scenarios;
    [SerializeField] private GameTileFactory _gameTileFactory;
    public State GetScenario(ISetterTile tile)
    {
        var spawner = _gameTileFactory.GetEnemySpawner();
        tile.SetTypeTile(TypeOfTile.SpawnerEnemy);
        tile.SetContentTile(spawner);
        return new State(this,spawner);
    }
    
    public struct State
    {
        private float _timeLast;
        private GameScenario _gameScenario;
        private SpawnerScenario.State[] _scenarios;
        public State(GameScenario gameScenario,EnemySpawner spawner)
        {
            _gameScenario = gameScenario;
            _timeLast = gameScenario._timeBetweenWaves;
            _scenarios = _gameScenario._scenarios
                .Select(scenario => scenario.Initialize(gameScenario._gameTileFactory,spawner))
                .ToArray();
        }

        public void ScenarioUpdate()
        {
            if (_timeLast <= 0 && CheckOnUpdatedScenarios())
            {
                for (int i = 0; i < _scenarios.Length; i++)
                    _scenarios[i].NextWave();
                _timeLast = _gameScenario._timeBetweenWaves;
            }

            _timeLast -= Time.deltaTime;
        }

        private bool CheckOnUpdatedScenarios()
        {
            for (int i = 0; i < _scenarios.Length; i++)
                if (!_scenarios[i].ScenarioUpdate())
                    return false;
            return true;
        }
    }
}