
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;
public class ScenariosLoader : ILoadingOperation
{
    private int _index = 0;
    public string Description => "Scenarios loading...";
    private List<GameScenarioJson> _gameScenarios = new List<GameScenarioJson>();
    public async UniTask Load(Action<float> onProcess)
    {
        float progress = 0;
        var path = PathCollection.PATHTOSCENARIOS;
        string[] jsonFiles = System.IO.Directory.GetFiles(path, "*.json");
        float progressCoefficient = 1f / jsonFiles.Length;
        foreach (var jsonPath in jsonFiles)
        {
            string jsonText = System.IO.File.ReadAllText(jsonPath);
            var scenario = JsonUtility.FromJson<GameScenarioJson>(jsonText);
            _gameScenarios.Add(scenario);
            onProcess?.Invoke(progress += progressCoefficient);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        _gameScenarios = _gameScenarios.OrderBy(x => x.Name).ToList();
        onProcess?.Invoke(1f);
    }

    public GameScenarioJson GetCurrentScenario()
    {
        return _gameScenarios[_index];
    }
}