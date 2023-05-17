using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSprite : MonoBehaviour
{
    public static LevelSprite manager;

    public Sprite wall;
    public Sprite path;
    public Sprite bumper;
    public Sprite end;
    public Sprite hole;

    public List<TileBase> levelElementTile;
    public TileBase bumperTile;

    void Awake()
    {
        manager = this;
    }
}
