using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public int moneyBuffer =0;
    private int amountDisplayed =0;
    public TMP_Text moneyCountUI;
    //public GameManager gameManager;

    private void FixedUpdate()
    {
        if(moneyBuffer > 0)
        {
            amountDisplayed += 1;
            moneyBuffer -= 1;
            moneyCountUI.text = amountDisplayed.ToString();
        }
        

    }

    public void setMoneyBuffer(int amount)
    {
        moneyBuffer = amount;
    }
}
