using System;
using Cysharp.Threading.Tasks;

public class EnemyInfoLoader : AssetLoader,ILoadingOperation
{
    public string Description => "Enemies infos is loading...";
    public UniTask Load(Action<float> onProcess)
    {
        throw new NotImplementedException();
    }
}