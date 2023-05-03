using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetTileSprite : MonoBehaviour
{
    public Tilemap map;
    public Tile tile;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            print(mousePosition);

            tile = (Tile)map.GetTile(map.WorldToCell(mousePosition));
            print(tile.sprite);
        }
    }
}
