
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginOperation : ILoadingOperation
{
    public string Description => "Login loading...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(1);
    }
}