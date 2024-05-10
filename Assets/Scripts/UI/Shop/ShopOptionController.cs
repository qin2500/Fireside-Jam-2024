using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopOptionController : MonoBehaviour
{
    public int itemId;
    public ShopController shop;
    public TMP_Text title;
    public Image image;
    public TMP_Text description;
    public TMP_Text price;


    public void makePurchase()
    {
        Debug.Log("Purchasing Tile with id: " + itemId);
        shop.makePurchase(this.itemId);
    }

    public void setTitle(string title)
    {
        this.title.SetText(title);
    }
    public void setImage(Sprite image)
    {
        this.image.sprite = image;
    }
    public void setDescription(string description)
    {
        this.description.SetText(description);
    }
    public void setPrice(int price)
    {
        this.price.SetText(price.ToString());
    }
    public void setItemId(int id)
    {
        this.itemId = id;
    }
    public void setShop(ShopController shop)
    {
        this.shop = shop; 
    }
}
