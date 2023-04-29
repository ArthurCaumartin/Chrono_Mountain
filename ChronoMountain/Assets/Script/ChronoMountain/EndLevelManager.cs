using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class EndLevelManager : MonoBehaviour
{
    //? Trois senario :
    //? - Win
    //? - Lose (timer)
    //? - Reset (lose sans timer)

    [SerializeField] Transform playerTransrom;
    [SerializeField] Tilemap levelMap;
    [SerializeField] Sprite endSprite;
    [SerializeField] Sprite pathSprite;
    [SerializeField] UnityEvent onWin;
    [SerializeField] UnityEvent onLose;

    [ContextMenu("GetPlayerTileSprite")]
    Sprite GetPlayerTileSprite()
    {
        Tile tile = (Tile)levelMap.GetTile(levelMap.WorldToCell(playerTransrom.position));
        // print(tile.sprite);
        return tile.sprite;
    }

    //! Call par un event sur player Movement a chaque fin de sequence de move
    public void EndLevelCheck()
    {
        if(GetPlayerTileSprite() == endSprite)
        {
            print("Level Complet !");
            onWin.Invoke();
        }
        else if(GetPlayerTileSprite() == pathSprite)
        {
            print("Level not Complete !");
            onLose.Invoke();
        }
    }


}
