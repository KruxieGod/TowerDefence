using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class CounterMoneyLoader : AssetLoader,ILoadingOperation
{
    private CounterMoneyUI _counterMoneyUI { get; set; }
    public CounterMoney CounterMoney { get; private set; } = new();
    public string Description { get; }
    public async UniTask Load(Action<float> onProcess)
    {
        _counterMoneyUI = await LoadAsync<CounterMoneyUI>(AddressableData.UI_COUNTER_MONEY);
        CounterMoney.SubscribeUI(_counterMoneyUI);
    }
}