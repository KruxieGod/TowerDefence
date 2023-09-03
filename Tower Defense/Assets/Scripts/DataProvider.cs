
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class DataProvider : ILoadingOperation
{
    public GameScenario GameScenario { get; private set; }
    public string Description => "Board initializing...";
    public UniTask Load(Action<float> onProcess)
    {
        return UniTask.Delay(1);
    }
}