using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int daysLeft = 1000;
    public int money;
    private int debt;
    
    public GridController gridController;
    public UIManager uiManager;
    
    public void advanceTurn()
    {
        //Evaluate the grid
        int gridValue = gridController.evaluateGrid();
        int[,] incomeTable = gridController.getIncomeTable();
        money += gridValue;

        //Update money ui
        uiManager.GetMoneyUI().updateMoneyUI(incomeTable, gridController.getRealWorldPosLookUp(), gridController.getCellSize());

        //Update debt and days left
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
