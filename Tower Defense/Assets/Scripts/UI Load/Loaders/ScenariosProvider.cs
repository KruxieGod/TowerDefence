
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;
public class ScenariosProvider
{
    private List<GameScenarioJson> _gameScenarios = new List<GameScenarioJson>();
    public async UniTask Load()
    {
        string[] jsonFiles = System.IO.Directory.GetFiles(PathCollection.PATHTOSCENARIOS, "*.json");
        foreach (var jsonPath in jsonFiles)
        {
            _gameScenarios.Add(JsonExtension.GetClassFromJson<GameScenarioJson>(jsonPath));
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        _gameScenarios = _gameScenarios.OrderBy(x => x.Name).ToList();
    }

    public GameScenarioJson GetCurrentScenario()
    {
        return _gameScenarios[0];
    }
}

