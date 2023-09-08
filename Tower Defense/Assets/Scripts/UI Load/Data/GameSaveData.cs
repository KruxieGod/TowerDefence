
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    [field : SerializeField]public List<LevelsSaveData> CreatedGames { get; private set; } =  new ();
}

[Serializable]
public class LevelsSaveData
{
    [field: SerializeField]public int CompletedLevels { get; private set; } = 0;
}