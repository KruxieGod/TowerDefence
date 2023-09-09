
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
public class ScenariosProvider : ILoadingOperation
{
    private int _currentIndex;
    private List<GameScenarioJson> _gameScenarios;
    public async UniTask Load(Action<float> onProcess = null)
    {
        _gameScenarios = JsonExtension
            .GetEnumerableClassFromJson<GameScenarioJson>(PathCollection.PATHTOSCENARIOS)
            .OrderBy(x => x.Name)
            .ToList();
    }

    public GameScenarioJson GetCurrentScenario()
    {
        return _gameScenarios[_currentIndex];
    }

    public string Description { get; }

    public void SetScenario(int index) => _currentIndex = --index;
}

