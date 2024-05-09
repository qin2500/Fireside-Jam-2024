using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLookUpManager : MonoBehaviour
{
    public TileInfo[] allTiles;
    private Dictionary<int, TileInfo> lookUpTable;

    private void Start()
    {
        foreach (TileInfo i in allTiles)
        {
            lookUpTable.Add(i.id, i);
        }
    }

    public TileInfo getTile(int id)
    {
        return lookUpTable[id];
    }
}
