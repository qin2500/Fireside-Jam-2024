using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TileInfo")]
public class TileInfo : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public int cost;
    public Tile tile;
    public string[] tags;
    public GameTile gameTile;
}   
