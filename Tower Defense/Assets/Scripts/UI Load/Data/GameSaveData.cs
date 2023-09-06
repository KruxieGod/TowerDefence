
using System.Collections.Generic;

public class GameSaveData
{
    public List<LevelsSaveData> CreatedGames { get; private set; }
}

public class LevelsSaveData
{
    public int CompletedLevels { get; private set; } = 0;
}