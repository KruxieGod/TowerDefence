using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class GameScenario : ScriptableObject
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private List<SpawnerScenario> _scenarios;
    [NonSerialized] private GameTileFactory _gameTileFactory;
    public State GetScenario(GameTileFactory tileFactory, GameBoard gameBoard)
    {
        _gameTileFactory = tileFactory;
        return new State(this,gameBoard);
    }

    public GameScenarioJson GetJsonClass()
    {
        return new GameScenarioJson(_timeBetweenWaves, _scenarios.Select(x => x.GetJsonClass()).ToList());
    }
    
    public struct State
    {
        private float _timeLast;
        private GameScenario _gameScenario;
        private SpawnerScenario.State[] _scenarios;
        public State(GameScenario gameScenario,GameBoard gameBoard)
        {
            _gameScenario = gameScenario;
            _timeLast = gameScenario._timeBetweenWaves;
            _scenarios = _gameScenario._scenarios
                ?.Select(scenario => scenario.Initialize(gameScenario._gameTileFactory,gameBoard))
                ?.ToArray();
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
            bool result = true;
            for (int i = 0; i < _scenarios.Length; i++)
                if (!_scenarios[i].ScenarioUpdate())
                    result =  false;
            return result;
        }
    }
}

[Serializable]
public class GameScenarioJson
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private List<SpawnerScenarioJson> _scenarios;

    public GameScenarioJson(float timeBetweenWaves,
        List<SpawnerScenarioJson> scenarios)
    {
        _timeBetweenWaves = timeBetweenWaves;
        _scenarios = scenarios;
    }
}