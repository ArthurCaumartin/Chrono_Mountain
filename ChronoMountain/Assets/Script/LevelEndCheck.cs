using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mwa.Chronomountain
{
    public class LevelEndCheck : MonoBehaviour
    {
        [SerializeField] PlayerEndLevelAnimation endAnimation;
        [SerializeField] ScriptableLevel level;
        bool isWin = false;
        public void TileCheck(Tile underPlayerTile)
        {
            if(underPlayerTile == LevelTile.instance.end)
                isWin = true;
            else
                isWin = false;
            
            endAnimation.StartAnimation(isWin, () =>
            {
                EndCheck(underPlayerTile);
            });
        }

        void EndCheck(Tile underPlayerTile)
        {
            // print("Level Check End tile = " + underPlayerTile);
            if(underPlayerTile == LevelTile.instance.end)
            {
                level.SetIsWin(true);
                InGameCanvasManager.manager.SetWin();
            }
            else
            {
                InGameCanvasManager.manager.SetLose();
            }
        }
    }
}