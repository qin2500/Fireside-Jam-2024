using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public TileInfo house;

    public Tilemap tileMap;
    public Tile mainGrass;  //Main grass tile
    public Tile[] grassDecorationTiles; //Set of grass decorations

    public int grid_width;
    public int grid_height;
    private int[,] incomeGrid;
    private TileInfo[,] grid;

    private System.Random random;
    public Vector2Int curMousePos; //Mouse pos in grid world
    private Vector2Int prvMousePos; //Used to keep track of the cursor's previous position
    [SerializeField]
    private GameObject hoverImage; //Image used for hover effect

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random(System.DateTime.UtcNow.ToString().GetHashCode());
        initGrid();
        drawGrid();
    }

    // Update is called once per frame
    void Update()
    {
        curMousePos = getMousePosition();

        //Highlight the tile we are hovering
        if(curMousePos != prvMousePos && !cursorOutsideBounds())
        {
            // Calculate the world position of the tile
            Vector3 tileWorldPos = tileMap.GetCellCenterWorld((Vector3Int)curMousePos);

            hoverImage.transform.position = new Vector3(tileWorldPos.x, tileWorldPos.y, (float)-0.5);

            // Update the previous tile coordinate
            prvMousePos = curMousePos;
        }

        if (cursorOutsideBounds())
            hoverImage.SetActive(false);
        else hoverImage.SetActive(true);

        if(Input.GetMouseButtonDown(0))
        {
            instantiateTile(house, curMousePos);
        }

    }

    void initGrid()
    {
        grid = new TileInfo[grid_width, grid_height];
        incomeGrid = new int[grid_width, grid_height];
        for (int i=0; i < grid.GetLength(0); i++)
        {
            for(int j=0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = null;
                incomeGrid[i, j] = 0;
            }
        }
    }

    void drawGrid()
    {
        tileMap.ClearAllTiles();
        for (int i= 0; i< grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                TileInfo gameTile = grid[i, j];

                //Get tile ojbect from id
                Tile tile = mainGrass;
                if (gameTile == null)
                {
                    if (random.Next(0, 100) > 80)
                    {
                        tile = grassDecorationTiles[random.Next(0, grassDecorationTiles.Length)];
                    }
                    else tile = mainGrass;
                }
                else tile = gameTile.tile;

                tileMap.SetTile(new Vector3Int(i-grid_width/2,j-grid_height/2,0), tile);
            }
        }
    }

    public int evaluateGrid()
    {
        int total = 0;
        for (int i=0; i<incomeGrid.GetLength(0); i++ )
        {
            for (int j=0; j<incomeGrid.GetLength(1); j++)
            {
                if (grid[i, j] == null) continue;
                Debug.Log(i + " | " + j);
                incomeGrid[i, j] = grid[i, j].gameTile.activate(new Vector2Int(i,j), grid, incomeGrid);
                total += incomeGrid[i, j];
            }
        }
        return total;
    }

    public void resetIncomeGrid()
    {
        for (int i = 0; i < incomeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < incomeGrid.GetLength(1); j++)
            {
                incomeGrid[i, j] = 0;
            }
        }
    }

    void instantiateTile(TileInfo tileInfo, Vector2Int gridWorldPos)
    {
        if (cursorOutsideBounds()) return;
        Vector2Int arrPos = gridToArrayPos(gridWorldPos);
        grid[arrPos.x, arrPos.y] = tileInfo;
        tileMap.SetTile(new Vector3Int(gridWorldPos.x, gridWorldPos.y, 0), tileInfo.tile);
    }


    private Boolean cursorOutsideBounds()
    {
        return curMousePos.x == -1000 || curMousePos.y == -1000;
    }

    Vector2Int getMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Vector3 worldPoint = hit.point;
            Vector3Int cellPosition = tileMap.WorldToCell(worldPoint);
            if (tileMap.GetTile(new Vector3Int(cellPosition.x, cellPosition.y, 0)) == null) goto OutOfBounds;
            return new Vector2Int(cellPosition.x, cellPosition.y);
        }
        OutOfBounds:
        return new Vector2Int(-1000, -1000);
    }

    Vector2Int gridToArrayPos(Vector2Int gridWorldPos)
    {
        return new Vector2Int(gridWorldPos.x + grid_width / 2, gridWorldPos.y + grid_height / 2);
    }

   
}
