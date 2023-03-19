using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public event UnityAction<int> MoneyAmountChanged;

    public int Money { get; private set; }
    public int TotalMoney { get; private set; }

    private void Start()
    {
        TotalMoney = Saver.Instance.SaveData.TotalMoney;
        Money = Saver.Instance.SaveData.CurrentMoney;
        MoneyAmountChanged?.Invoke(Money);
    }

    public void AddCurrency(int amount)
    {
        Money += amount;
        TotalMoney += amount;
        Saver.Instance.SaveTotalMoney(TotalMoney);
        MoneyAmountChanged?.Invoke(Money);
    }

    public void RemoveCurrency(int amount)
    {
        Money -= amount;
        MoneyAmountChanged?.Invoke(Money);
    }

    public void Cheat()
    {
        Money += 10000;
        MoneyAmountChanged?.Invoke(Money);
    }

    public void SetTotalMoney(int score)
    {
        if (score <= TotalMoney)
            return;

        TotalMoney = score;
    }

    public void AddReward(int rewardAmount)
    {
        if (rewardAmount <= 0)
            return;

        Money = Saver.Instance.SaveData.CurrentMoney + rewardAmount;
        TotalMoney = Saver.Instance.SaveData.TotalMoney + rewardAmount;
        Saver.Instance.SaveTotalMoney(TotalMoney);
        Saver.Instance.SaveCurrentMoney(Money);
    }
}