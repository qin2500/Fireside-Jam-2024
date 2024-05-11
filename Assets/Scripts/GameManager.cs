using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int daysLeft;
    private int day = 0;
    public int money;
    private int debt;
    private int totalMoneyMade = 0;

    public int[] payments;
    public int[] days;
    private int paymentIndex =0;

    public GameObject nextTurnButton;
    public GridController gridController;
    public UIManager uiManager;
    public ShopController shopController;
    public DayTrackerController dayTrackerController;

    public DebtInfoController debtInfoMenu;
    public GameObject IndrocutionMenu;
    public GameObject GameOverMneu;
    public GameObject DebtPaymentMenu;
    public GameObject WinMenu;

    private void Start()
    {
        IndrocutionMenu.SetActive(true);
        nextTurnButton.SetActive(false);
        debtInfoMenu.deactivate();
        dayTrackerController.setDay(0);
        dayTrackerController.setPayment(1, payments.Length);
    }

    public void advanceTurn()
    {
        if (uiManager.GetMoneyUI().getIsUpdating()) return;
        Debug.Log("Advancing to next day...");
        //Evaluate the grid
        int gridValue = gridController.evaluateGrid();
        int[,] incomeTable = gridController.getIncomeTable();
        money += gridValue;
        totalMoneyMade+= gridValue;

        //Update money ui
        uiManager.GetMoneyUI().updateMoneyUI(incomeTable, gridController.getRealWorldPosLookUp(), gridController.getCellSize());


        //Update debt and days left
        daysLeft -= 1;
        day += 1;
        dayTrackerController.setDay(day);
        
        

    }

    public void checkDebt()
    {
        if (daysLeft == 0)
        {
            //Handle paying debt
            if (money >= debt)
            {
                money -= debt;
                uiManager.GetMoneyUI().increaseMoneyBuffer(debt * -1);

                paymentIndex++;
                if (paymentIndex >= payments.Length)
                {
                    Debug.Log("YOU WIN!!");
                    WinMenu.SetActive(true);
                }
                debt = payments[paymentIndex];
                daysLeft = days[paymentIndex];
                dayTrackerController.setPayment(paymentIndex + 1, payments.Length);
            }
            else
            {
                Debug.Log("Lmao dumb bitch is broke");
                GameOverMneu.SetActive(true);
                DeathMenuController d = GameOverMneu.GetComponent<DeathMenuController>();
                d.setDays(day);
                d.setMoney(totalMoneyMade);
            }
        }
        updateGameInfoUI();
        openShop();
    }

    public void addMoney(int amount)
    {
        money += amount; 
    }

    public void startGame()
    {
        IndrocutionMenu.SetActive(false);
        nextTurnButton.SetActive(true);
        debtInfoMenu.activate();

        debt = payments[paymentIndex];
        daysLeft = days[paymentIndex];
        updateGameInfoUI();
    }

    public void updateGameInfoUI()
    {
        debtInfoMenu.setAmountDue(debt);
        debtInfoMenu.setDaysLeft(daysLeft);
    }

    public void openShop()
    {
        shopController.open();
        debtInfoMenu.deactivate();
    }
    public int getMoney()
    {
        return money;
    }
}
