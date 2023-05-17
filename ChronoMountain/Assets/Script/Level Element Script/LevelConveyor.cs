using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Mwa.Chronomountain
{
    public class LevelConveyor : LevelElementBase
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        //TODO je sais pas komen fair :( sa va etre moche mais tanpis !!
    
        public override void OnStep(System.Action callBack)
        {
            DOTween.To((lerpT) =>{

            },
            0, 1, 5).OnComplete(() =>
            {
                if(callBack != null)
                    callBack();
            });
        }
        
        // int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition, out LevelElementBase levelElementTile)
        // {//* le gro bordel
        //     int distance = 0;
        //     TileBase targetTile = levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));

        //     for(int i = 1; i < 100; i++) 
        //     {
        //         Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
        //         targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
        //         // print(targetTile);

        //         if(createDebugTarget && Application.isEditor)
        //             Instantiate(debugTarget, targetToCheck, Quaternion.identity);

        //         LevelElementBase le = LevelElementBase.GetAt(targetToCheck);
        //         if(le) {
        //             levelElementTile = le;
        //             distance = i;
        //             return distance;
        //         }

        //         if(targetTile == LevelTile.instance.wall)
        //         {
        //             levelElementTile = null;
        //             distance = i;
        //             return distance - 1;
        //         }
        //     }
        //     levelElementTile = null;
        //     return 0;
        // }
    }
}
