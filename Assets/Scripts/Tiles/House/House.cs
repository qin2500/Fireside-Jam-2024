using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/House")]
public class House : GameTile
{
    public override int activate(Vector2Int position, TileInfo[,] grid, int[,] incomeGrid)
    {
        Debug.Log("Evaluating Hosue at : i = " + position.x + " and j = " + position.y  );
        int count = 0;
        int i = position.x;
        int j = position.y;
        string tag = "House";

        if(i-1 >= 0)
        {
            count += permuteJ(i-1, j, grid, tag);
        }
        if(i+1 < grid.GetLength(1))
        {
            count += permuteJ(i + 1, j, grid, tag);
        }
        count += permuteJ(i,j,grid, tag);

        return count;
    }

    private int permuteJ (int i, int j, TileInfo[,] grid, string tag)
    {
        Debug.Log(i + " | " + j);
        int count = 0;
        if (grid[i,j] != null && grid[i, j].tags.Contains(tag)) count++;
        if (j - 1 >= 0 && grid[i, j-1] != null && grid[i, j - 1].tags.Contains(tag)) count++;
        if (j + 1 < grid.GetLength(1) && grid[i, j+1] != null && grid[i, j + 1].tags.Contains(tag)) count++;

        return count;
    }
}
