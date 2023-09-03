
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginOperation : ILoadingOperation
{
    public string Description => "Login loading...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.3f);
        Debug.Log("LoginOperation in");
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        onProcess?.Invoke(0.66f);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        Debug.Log("LoginOperation is initialized");
        onProcess?.Invoke(1);
    }
}