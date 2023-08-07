using System;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    public readonly Func<Tile, Tile, bool> CheckOn;
    
    public Checker(Func<Tile,Tile,bool> check)
    {
        CheckOn = check;
    }
}
