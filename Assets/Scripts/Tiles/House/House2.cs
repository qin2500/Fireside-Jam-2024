using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/House2")]
public class House2 : GameTile
{
    public override int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid)
    {
        return 10;
    }
}
