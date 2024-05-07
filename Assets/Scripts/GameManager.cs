using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int daysLeft = 1000;
    private int money;
    private int debt;
    
    public GridController gridController;
    public UIManager uiManager;
    
    public void advanceTurn()
    {
        int gridValue = gridController.evaluateGrid();
        money += gridValue;
        uiManager.setMoneyBuffer(gridValue);

        daysLeft -= 1;
        if(daysLeft == 0)
        {
            //Handle paying debt
            if(money >= debt)
            {
                money -= debt;
            }
            else
            {
                Debug.Log("Lmao dumb bitch is broke");
            }
        }

    }
}
