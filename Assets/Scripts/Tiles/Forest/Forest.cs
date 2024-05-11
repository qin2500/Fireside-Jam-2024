using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/Forest")]
public class Forest : GameTile
{
    public override int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid)
    {
        return 1;
    }
}
