using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTile : ScriptableObject, Activatable
{
    public abstract int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid);
}
