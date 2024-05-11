using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenuController : MonoBehaviour
{
    public TMP_Text money;
    public TMP_Text days;

    public void setMoney(int money)
    {
        this.money.SetText(money.ToString());
    }

    public void setDays(int days)
    { this.days.SetText(days.ToString());}
}
