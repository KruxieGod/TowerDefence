using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameScenario : ScriptableObject
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] List<SpawnerScenario> _scenarios;

    public State GetScenario(Tile tile)
    {
        
        return new State(this);
    }
    
    public struct State
    {
        private readonly float _timeBetweenWaves;
        private float _timeLast;
        private GameScenario _gameScenario;
        public State(GameScenario gameScenario)
        {
            _gameScenario = gameScenario;
            _timeBetweenWaves = gameScenario._timeBetweenWaves;
            _timeLast = _timeBetweenWaves;
        }

        public void ScenarioUpdate()
        {
            if (_timeLast <= 0)
            {
                
            }

            _timeLast -= Time.deltaTime;
        }
    }
}