using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mwa.Chronomountain
{
    public class TileDistance : MonoBehaviour
    {
        public static TileDistance instance;
        [SerializeField] bool createDebugTarget;
        [SerializeField] GameObject debugTarget;
        [SerializeField] Tilemap levelPathTileMap;

        void Awake()
        {
            instance = this;
        }

        //TODO Prendre en compte les tiles de vide
        public int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition, out LevelElementBase levelElementTile)
        {//* le gro bordel
            int distance = 0;
            TileBase targetTile = levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));

            for(int i = 1; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
                // print(targetTile);

                if(createDebugTarget && Application.isEditor)
                    Instantiate(debugTarget, targetToCheck, Quaternion.identity);

                LevelElementBase levelElementHit = LevelElementBase.GetAt(targetToCheck);
                if(levelElementHit)
                {
                    levelElementTile = levelElementHit;
                    distance = i;
                    return distance;
                }

                if(targetTile == LevelTile.instance.wall)
                {
                    levelElementTile = null;
                    distance = i;
                    return distance - 1;
                }
            }
            levelElementTile = null;
            return 0;
        }

        public int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition)
        {//* le gro bordel
            int distance = 0;
            TileBase targetTile = levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));

            for(int i = 1; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
                // print(targetTile);

                if(createDebugTarget && Application.isEditor)
                    Instantiate(debugTarget, targetToCheck, Quaternion.identity);

                LevelElementBase levelElementHit = LevelElementBase.GetAt(targetToCheck);
                if(levelElementHit)
                {
                    distance = i;
                    return distance;
                }

                if(targetTile == LevelTile.instance.wall)
                {
                    distance = i;
                    return distance - 1;
                }
            }
            return 0;
        }



        
    }
}
