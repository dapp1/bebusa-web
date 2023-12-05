using System;
using UnityEngine;

public static class Money
{
    public static int money
    {
        get { return PlayerPrefs.GetInt("money"); }
        private set
        {
            PlayerPrefs.SetInt("money", value);
            OnUpdateMoney?.Invoke(value);
        }
    }

    public static int token
    {
        get { return PlayerPrefs.GetInt("token"); }
        private set
        {
            PlayerPrefs.SetInt("token", value);
            OnUpdateToken?.Invoke(value);
        }
    }

    public static event UpdateMoneyHandler OnUpdateMoney;
    public static Action<float> OnUpdateToken;

    public static bool SpendMoney(int moneySpend)
    {
        if (money >= moneySpend)
        {
            money -= moneySpend;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SetMoney(int moneySet)
    {
        money = moneySet;
    }

    public static void SetToken(int tokenSet)
    {
        token = tokenSet;
    }

    public static bool GainMoney(int moneyGain)
    {
        money += moneyGain;
        return true;
    }

    public static bool SpendToken(int tokenSpend)
    {
        if (token >= tokenSpend)
        {
            token -= tokenSpend;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool GainToken(int tokenGain)
    {
        token += tokenGain;
        return true;
    }

}

public delegate void UpdateMoneyHandler(float money);
