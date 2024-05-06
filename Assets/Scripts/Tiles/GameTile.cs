using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameTile
{
    public int activate(Vector2Int position, TileInfo[,] grid);
}
