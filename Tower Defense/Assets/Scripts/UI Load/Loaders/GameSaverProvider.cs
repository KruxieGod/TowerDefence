
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSaverProvider : ILoadingOperation
{
    private GameSaveData _gameSaveData { get; set; }

    private int _count
    {
        get
        {
            if (_gameSaveData == null)
                return 0;
            return _gameSaveData.CreatedGames.Count;
        }
    }
    public string Description { get; }
    public UniTask Load(Action<float> onProcess = null)
    {
        _gameSaveData = JsonExtension.GetClassFromJson<GameSaveData>(PathCollection.PATHTOSAVES);
        Debug.Log(_gameSaveData?.CreatedGames?.Count);
        return UniTask.CompletedTask;
    }

    public int GetCurrentGames() => _count;

    public LevelsSaveData CreateNewGame()
    {
        var levels = new LevelsSaveData();
        if (_gameSaveData == null)
            _gameSaveData = new GameSaveData();
        _gameSaveData.CreatedGames.Add( levels);
        Serialize();
        return levels;
    }

    private void Serialize()
    {
        JsonExtension.SerializeClass(_gameSaveData,PathCollection.PATHTOSAVES);
    }

    public LevelsSaveData GetLevels(int index) => _gameSaveData.CreatedGames[index];
}