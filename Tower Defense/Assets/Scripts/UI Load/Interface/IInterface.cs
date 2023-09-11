using UnityEngine;

public interface IInterface
{
    CanvasGroup GetCanvasGroup(GameResult gameResult);
    void ToNext();
    string AddressableName { get; }
}