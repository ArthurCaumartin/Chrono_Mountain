using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public static LevelTile instance;

    public TileBase wall;
    public TileBase path;
    public TileBase end;
    public TileBase hole;
    public TileBase coveyorUp;
    public TileBase coveyorRight;
    public TileBase coveyorLeft;
    public TileBase coveyorDown;

    public List<TileBase> levelElementTile;

    void Awake()
    {
        instance = this;
    }
}
