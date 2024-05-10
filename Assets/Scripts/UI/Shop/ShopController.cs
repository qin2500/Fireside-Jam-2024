using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public TileLookUpManager tileLookUp;
    public GridController gridController;
    public GameManager gameManager;
    public ShopOptionController optionSlot1;
    public ShopOptionController optionSlot2;
    public ShopOptionController optionSlot3;

    private void Awake()
    {
        optionSlot1.setShop(this.gameObject.GetComponent<ShopController>());
        optionSlot2.setShop(this.gameObject.GetComponent<ShopController>());
        optionSlot3.setShop(this.gameObject.GetComponent<ShopController>());
    }

    public void generateOptions()
    {
        Debug.Log("Generating Shop Options");
        generateOption(optionSlot1);
        generateOption(optionSlot2);
        generateOption(optionSlot3);
    }

    public void open()
    {
        this.gameObject.SetActive(true);
        generateOptions();
        gridController.newTurnButton.SetActive(false);
    }

    public void close()
    {
        this.gameObject.SetActive(false);
    }

    public void makePurchase(int id)
    {
        gridController.setBuildMode(tileLookUp.getTile(id));
        gameManager.uiManager.GetMoneyUI().setMoneyBuffer(tileLookUp.getTile(id).cost * -1);
        close();
    }

    public TileInfo weightedRandomSelect()
    {
        TileInfo[] tiles = tileLookUp.allTiles;
        int weightSum = 0;
        foreach (TileInfo i in tiles) weightSum += i.weight;

        int randomWeight = Random.Range(0, weightSum);
        TileInfo selectedTile = tiles[0];
        foreach (TileInfo i in tiles)
        {
            if(randomWeight <= 0)
            {
                selectedTile = i;
            }
            randomWeight -= i.weight;
        }
        return selectedTile;
    }

    private void generateOption(ShopOptionController option)
    {
        TileInfo tile1 = weightedRandomSelect();
        option.setDescription(tile1.description);
        option.setImage(tile1.tile.sprite);
        option.setItemId(tile1.id);
        option.setPrice(tile1.cost);
        option.setTitle(tile1.name);
    }
}
