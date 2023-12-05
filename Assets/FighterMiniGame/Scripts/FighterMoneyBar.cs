using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FighterMoneyBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public int money;
    public static FighterMoneyBar instance;

    private void Awake()
    {
        money = 0;
        instance = this;
        Display();
    }

    public void Display()
    {
        moneyText.text = Money.money.ToString();
    }
}
