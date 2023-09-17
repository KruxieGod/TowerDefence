public class CounterMoney
{
    private CounterMoneyUI _counterMoneyUI;
    public int AmountMoney { get; private set; }
    public CounterMoney(CounterMoneyUI counterMoneyUI) => _counterMoneyUI = counterMoneyUI;

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
        return true;

    }
}