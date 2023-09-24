
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class TowerInfoLoader : ILoadingOperation
{
    private readonly Dictionary<string, TowerData<BehaviourTower>> _dataLasers = new ();
    private readonly Dictionary<string, TowerData<BehaviourBallistics>> _dataBallistics = new ();
    public string Description => "Tower info is loading...";
    async UniTask ILoadingOperation.Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);
        foreach (var towerInfo in JsonExtension.
                     GetEnumerableClassFromJson<TowerData<BehaviourTower>>(PathCollection.PATH_TO_TOWERS))
            _dataLasers.TryAdd(towerInfo.NameTower, towerInfo);
        foreach (var towerInfo in JsonExtension.
                     GetEnumerableClassFromJson<TowerData<BehaviourBallistics>>(PathCollection.PATH_TO_TOWERS))
            _dataBallistics.TryAdd(towerInfo.NameTower, towerInfo);
        onProcess?.Invoke(1f);
    }

    public int GetPrice(string nameTower)
    {
        if (_dataLasers.TryGetValue(nameTower, out var priceData))
            return priceData.Price;
        if (_dataBallistics.TryGetValue(nameTower, out var priceDat))
            return priceDat.Price;
        throw new ArgumentException("Tower Name is not found: " +nameTower);
    }
    
    public TBehaviour GetBehaviour<TBehaviour>(string nameTower)
    where TBehaviour : BehaviourTower
    {
        var a = typeof(TBehaviour) == typeof(BehaviourTower);
        var b = typeof(TBehaviour) == typeof(BehaviourBallistics);
        if (a)
            if (_dataLasers.TryGetValue(nameTower, out var priceData))
                return priceData.BehaviourTower as TBehaviour;
        if (b)
            if (_dataBallistics.TryGetValue(nameTower, out var priceDat))
                return priceDat.BehaviourTower as TBehaviour;
        throw new ArgumentException("Tower Name is not found: " +nameTower);
    }
}