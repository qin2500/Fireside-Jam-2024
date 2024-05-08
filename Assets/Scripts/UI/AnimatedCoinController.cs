using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatedCoinController : MonoBehaviour
{
    public TMP_Text moneyAmountUI;

    public void setMoneyAmount(int amount)
    {
        moneyAmountUI.SetText(amount.ToString());
    }
}
