using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public static LevelTile instance;

    public Tile wall;
    public Tile door;
    public Tile Key;
    public Tile end;
    public Tile hole;


    void Awake()
    {
        instance = this;
    }
}
