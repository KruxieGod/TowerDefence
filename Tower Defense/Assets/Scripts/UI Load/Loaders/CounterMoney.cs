public class CounterMoney
{
    private CounterMoneyUI _counterMoneyUI;
    public int AmountMoney { get; private set; }

    public void SubscribeUI(CounterMoneyUI counterMoneyUI)
    {
        _counterMoneyUI = counterMoneyUI;
        Reset();
    }

    public void AddMoney(int money)
    {
        AmountMoney += money;
        _counterMoneyUI.SetMoney(AmountMoney);
    }

    public bool EnoughMoney(int money)
    {
        if (AmountMoney - money < 0)
            return false;
        AmountMoney -= money;
        _counterMoneyUI.SetMoney(AmountMoney);
        return true;
    }

    public void Reset()
    {
        AmountMoney = 0;
        if (_counterMoneyUI != null)
            _counterMoneyUI.SetMoney(AmountMoney);
    }
}