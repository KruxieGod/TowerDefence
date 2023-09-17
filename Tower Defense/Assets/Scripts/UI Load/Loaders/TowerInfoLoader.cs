
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class TowerInfoLoader : ILoadingOperation
{
    private Dictionary<string, PriceData> _data = new ();
    public string Description => "Tower info is loading...";
    async UniTask ILoadingOperation.Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);
        foreach (var towerInfo in JsonExtension.
                     GetEnumerableClassFromJson<PriceData>(PathCollection.PATH_TO_TOWERS))
            _data.TryAdd(towerInfo.NameTower, towerInfo);
        onProcess?.Invoke(1f);
    }

    public int GetPrice(string nameTower)
    {
        if (_data.TryGetValue(nameTower, out var priceData))
            return priceData.Price;
        throw new ArgumentException("Tower Name is not found: " +nameTower);
    }
}