
using System;
using UnityEngine;

[Serializable]
public struct PriceData
{
    [field : SerializeField] public int Price { get; private set; }
}