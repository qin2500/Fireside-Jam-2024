using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/Lake")]
public class Lake : GameTile
{
    public override int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid)
    {
        return 1;
    }
}
