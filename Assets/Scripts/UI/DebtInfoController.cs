using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebtInfoController : MonoBehaviour
{
    public TMP_Text amountDue;
    public TMP_Text daysLeft;

    public void activate()
    {
        this.gameObject.SetActive(true);
    }
    public void deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void setAmountDue(int amount)
    {
        this.amountDue.SetText(amount.ToString());
    }

    public void setDaysLeft(int amount)
    {
        this.daysLeft.SetText(amount.ToString() + " days");
    }
}
