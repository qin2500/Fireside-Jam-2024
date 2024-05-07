using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Activatable
{
    public int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid);
}
