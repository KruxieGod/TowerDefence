using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu]
public class GameScenario : ScriptableObject
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private List<SpawnerScenario> _scenarios;
    [NonSerialized] private GameTileFactory _gameTileFactory;
    [SerializeField] private CountTiles _countTiles;
    private GameScenarioJson GetJsonClass()
    {
        return new GameScenarioJson(_timeBetweenWaves,
            _scenarios.Select(x => x.GetJsonClass()).ToList(),
            name,
            _countTiles);
    }

    private void OnDisable() => Serialization();
    private void OnValidate() => Serialization();
    private void OnEnable() => Serialization();

    
    private void Serialization()
    {
        var gameScenario = GetJsonClass();
        JsonExtension.SerializeClass(gameScenario,PathCollection.PATHTOSCENARIOS + name + ".json");
    }
}

[Serializable]
public class GameScenarioJson
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private List<SpawnerScenarioJson> _scenarios;
    [field : SerializeField] public string Name { get; private set; }
    [field: SerializeField] public CountTiles CountTiles { get; private set; }
    [Inject] private GameTileFactory _gameTileFactory;
    [Inject] private GameFactories _gameFactories;
    [Inject] private CounterMoney _counterMoney;
    [Inject] private AppearingWindowLoader _appearingWindowLoader;
    [Inject] private WinLoader _winLoader;
    public GameScenarioJson(float timeBetweenWaves,
        List<SpawnerScenarioJson> scenarios,
        string name,
        CountTiles countTiles)
    {
        CountTiles = countTiles;
        Name = name;
        _timeBetweenWaves = timeBetweenWaves;
        _scenarios = scenarios;
    }

    public State GetScenario(GameBoard gameBoard) => new (this, gameBoard);
    
    public struct State
    {
        private float _timeLast;
        private GameScenarioJson _gameScenario;
        private SpawnerScenarioJson.State[] _scenarios;
        public State(GameScenarioJson gameScenario,GameBoard gameBoard)
        {
            _gameScenario = gameScenario;
            _timeLast = gameScenario._timeBetweenWaves;
            _scenarios = _gameScenario._scenarios
                ?.Select(scenario => scenario.Initialize(gameBoard,gameScenario._gameTileFactory,gameScenario._gameFactories,gameScenario._counterMoney))
                ?.ToArray();
        }

        public void ScenarioUpdate()
        {
            if (_timeLast <= 0 && CheckOnUpdatedScenarios())
            {
                bool isContinued = false;
                for (int i = 0; i < _scenarios.Length; i++)
                    isContinued = _scenarios[i].NextWave() || isContinued;
                if (!isContinued)
                    _gameScenario._appearingWindowLoader.LoadState(_gameScenario._winLoader);
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