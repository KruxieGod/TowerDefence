using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

public class GameObjectsProvider : ILoadingOperation
{
    public GameManager GameManager { get; private set; }
    public string Description => "Manager is loading...";
    public async UniTask Load(Action<float> onProcess = null)
    {
        GameManager =  Object.FindAnyObjectByType<GameManager>();
    }
}