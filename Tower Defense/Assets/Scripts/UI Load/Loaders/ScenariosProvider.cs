
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;
public class ScenariosProvider : ILoadingOperation
{
    private List<GameScenarioJson> _gameScenarios = new List<GameScenarioJson>();
    public async UniTask Load(Action<float> onProcess = null)
    {
        foreach (var gameScenario in JsonExtension.GetEnumerableClassFromJson<GameScenarioJson>(PathCollection.PATHTOSCENARIOS))
        {
            _gameScenarios.Add(gameScenario);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        _gameScenarios = _gameScenarios.OrderBy(x => x.Name).ToList();
    }

    public GameScenarioJson GetCurrentScenario()
    {
        return _gameScenarios[0];
    }

    public string Description { get; }
}

