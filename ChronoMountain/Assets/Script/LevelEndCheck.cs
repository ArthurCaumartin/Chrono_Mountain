using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mwa.Chronomountain
{
    public class LevelEndCheck : MonoBehaviour
    {
        [SerializeField] ScriptableLevel level;
        public void EndCheck(Tile underPlayerTile)
        {
            // print("Level Check End tile = " + underPlayerTile);
            if(underPlayerTile.sprite == LevelSprite.manager.end)
            {
                // print("Win");
                level.SetIsWin(true);
                InGameCanvasManager.manager.SetWin();
            }
            else
            {
                // print("Lose");
                InGameCanvasManager.manager.SetLose();
            }
        }
    }
}