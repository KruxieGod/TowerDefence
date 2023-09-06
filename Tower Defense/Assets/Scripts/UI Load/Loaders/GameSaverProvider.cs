
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameSaverProvider : ILoadingOperation
{
    public GameSaveData GameSaveData { get; private set; }

    private int _count
    {
        get
        {
            if (GameSaveData == null)
                return 0;
            return GameSaveData.CreatedGames.Count;
        }
    }
    public string Description { get; }
    public UniTask Load(Action<float> onProcess = null)
    {
        GameSaveData = JsonExtension.GetClassFromJson<GameSaveData>(PathCollection.PATHTOSAVES);
        return UniTask.CompletedTask;
    }

    public int GetCurrentGames() => _count;
    
}