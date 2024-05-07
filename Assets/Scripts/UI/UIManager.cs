using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MoneyUI moneyUI;

    public void setMoneyBuffer(int amount)
    {
        moneyUI.setMoneyBuffer(amount);
    }
}
