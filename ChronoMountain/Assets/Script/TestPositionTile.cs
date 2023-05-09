using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TestPositionTile : MonoBehaviour
{
    public Tilemap map;
    public Sprite tileSprite;

    void Update()
    {
        Tile targetTile = (Tile)map.GetTile(map.WorldToCell(transform.position));
        tileSprite = targetTile.sprite;
    }
}
