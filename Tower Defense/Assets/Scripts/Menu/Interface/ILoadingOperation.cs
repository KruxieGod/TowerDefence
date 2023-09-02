using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ILoadingOperation
{
    string Description { get; }
    UniTask Load<T>(Action<float> onProcess);
}
