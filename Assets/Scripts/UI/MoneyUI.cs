using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyUI : MonoBehaviour
{
    public int moneyBuffer =0;
    private int amountDisplayed =0;
    public TMP_Text moneyCountUI;

    public GameObject animatedCoin;
    public int coinPoolSize;
    public Transform coinUIPosition;
    public float coinAnimationLength;
    public float coinAnimationDelay;
    private Queue<GameObject> coinPool = new Queue<GameObject>();

    //Used to keep track of which coins should be spawned first
    private Dictionary<int, List<Vector2>> coinValuePools = new Dictionary<int, List<Vector2>>();
    private Queue<int> valuePoolsList = new Queue<int>();
    private int currentBatchSize;
    private int batchCount;


    private void Awake()
    {
        //Fills our coin pool
        for (int i=0; i<coinPoolSize; i++)
        {
            GameObject coin = Instantiate(animatedCoin);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }
    }

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

    public void increaseMoneyBuffer(int ammount)
    {
        moneyBuffer += ammount;
    }

    public void updateMoneyUI(int[,] incomeGrid, Vector2[,] worldLoopUp, float cellSize)
    {
        generateValuePools(incomeGrid, worldLoopUp);
        updateMoney(cellSize);
    }

    private void updateMoney(float cellSize)
    {
        Debug.Log(valuePoolsList.ToCommaSeparatedString());
        if(valuePoolsList.Count > 0)
        {
            batchCount ++ ;

            if (batchCount >= currentBatchSize)
            {
                int amount = valuePoolsList.Dequeue();
                List<Vector2> curCoinPool = coinValuePools[amount];
                currentBatchSize = curCoinPool.Count;
                batchCount = 0;
                foreach (Vector2 i in curCoinPool)
                {
                    animateCoin(new Vector3(i.x, i.y, -1), amount, cellSize);
                }
            }
            
        }
        else
        {
            batchCount = 0;
            currentBatchSize = 0;
            coinValuePools.Clear();
            valuePoolsList.Clear();
        }
    }

    private void animateCoin(Vector3 startPosition, int ammount, float cellSize)
    {
        if(coinPool.Count > 0)
        {
            GameObject coin = coinPool.Dequeue();
            coin.transform.localPosition = new Vector3(startPosition.x + cellSize/2, startPosition.y + cellSize/2, -1);
            coin.SetActive(true);

            AnimatedCoinController coinController = coin.GetComponent<AnimatedCoinController>();
            coinController.setMoneyAmount(ammount);

            coin.transform.DOMove(getUiWorldPosition(coinUIPosition.position), coinAnimationLength)
                .SetDelay(coinAnimationDelay)
                .OnComplete(() =>   
                {
                    coin.SetActive(false);
                    coinPool.Enqueue(coin);
                    increaseMoneyBuffer(ammount);
                    updateMoney(cellSize);
                });
        }
        
    }

    public void generateValuePools(int[,] incomeTable, Vector2[,] worldLookUp)
    {
        for(int i=0; i<incomeTable.GetLength(0); i++)
        {
            for (int j = 0; j < incomeTable.GetLength(1); j++)
            {
                if (incomeTable[i, j] == 0) continue;
                Vector2 worldPos = worldLookUp[i, j];
                if (!coinValuePools.ContainsKey(incomeTable[i, j]))
                {
                    coinValuePools.Add(incomeTable[i, j], new List<Vector2>());
                    valuePoolsList.Enqueue(incomeTable[i, j]);
                }
                coinValuePools[incomeTable[i, j]].Add(worldPos);
                
            }
        }
        List<int> temp = valuePoolsList.ToList();
        temp.Sort();
        valuePoolsList = new Queue<int>(temp);



    }

    public Vector3 getUiWorldPosition(Vector3 uiPosition)
    {
        Vector3 worldPosition = new Vector3(uiPosition.x, uiPosition.y, 0);
        worldPosition = Camera.main.ScreenToWorldPoint(worldPosition);
        return worldPosition;
    }
}
