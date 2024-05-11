using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayTrackerController : MonoBehaviour
{
    public TMP_Text day;
    public TMP_Text payment;

    public void setDay(int day)
    {
        this.day.SetText(day.ToString());
    }

    public void setPayment(int num, int total)
    {
        payment.SetText(num.ToString() + "/" + total.ToString());
    }
}
