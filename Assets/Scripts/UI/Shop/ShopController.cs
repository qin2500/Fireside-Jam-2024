using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public TileLookUpManager tileLookUp;
    public ShopOptionController optionSlot1;
    public ShopOptionController optionSlot2;
    public ShopOptionController optionSlot3;


    public void generateOptions()
    {
        Debug.Log("Generating Shop Options");
    }
}
