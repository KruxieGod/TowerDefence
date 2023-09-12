
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct CountTiles 
{
    [field: SerializeField]public int Walls { get; private set; }
    [field: SerializeField]public int Destinations{ get; private set; }
    [field: SerializeField]public int Lasers{ get; private set; }
    [field: SerializeField]public int Mortars{ get; private set; }
}