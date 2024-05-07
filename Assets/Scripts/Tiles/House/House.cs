using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/House")]
public class House : GameTile
{
    public override int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid)
    {
        return 1;
    }
}
