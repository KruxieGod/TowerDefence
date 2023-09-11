
using System;

public class GameEvents
{
    public GameEvents(Action<IInterface> action) => OnGameState += action;

    private GameEvents(){ }

    public Action<IInterface> OnGameState { get; set; }
}