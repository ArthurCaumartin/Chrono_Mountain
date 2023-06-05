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
        // public bool levelHasDoor = false;
        public bool isPlayerWayOnKey = false;

        void Awake()
        {
            instance = this;
        }

        //TODO Prendre en compte les tiles de vide
        //! Call par les deplacament du joueur
        public int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition)
        {//* le gro bordel *edit* => en vrais ça va...
            int distance = 0;
            TileBase targetTile = levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));

            for(int i = 1; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
                // print(targetTile);

                if(createDebugTarget && Application.isEditor)
                {
                    Instantiate(debugTarget, targetToCheck, Quaternion.identity);
                }

                //! regarde si le joueut passe sur une key
                if(targetTile == LevelTile.instance.Key)
                {
                    isPlayerWayOnKey = true;
                }

                //! Regarde si on passe sur une porte
                if(targetTile == LevelTile.instance.door)
                {
                    if(isPlayerWayOnKey == false)
                    {
                        distance = i;
                        return distance - 1;
                    }
                }
                
                //! Regarde si on passe sur un level element
                LevelElementBase levelElementHit = LevelElementBase.GetAt(targetToCheck);
                if(levelElementHit)
                {
                    distance = i;
                    return distance;
                }

                //! Regarde si on passe sur un mur
                if(targetTile == LevelTile.instance.wall)
                {
                    distance = i;
                    return distance - 1;
                }

                if(targetTile == LevelTile.instance.water)
                {
                    print("On water !");
                    distance = i;
                    return distance;
                }
            }
            return 0;
        }


        //! Call par les Level element
        // public int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition)
        // {//* le gro bordel
        //     int distance = 0;
        //     TileBase targetTile = levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));

        //     for(int i = 1; i < 100; i++) 
        //     {
        //         Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
        //         targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
        //         // print(targetTile);

        //         if(createDebugTarget && Application.isEditor)
        //         {
        //             Instantiate(debugTarget, targetToCheck, Quaternion.identity);
        //         }

        //         if(targetTile == LevelTile.instance.Key)
        //         {
        //             isPlayerWayOnKey = true;
        //         }

        //         //! Regarde si on passe sur une porte
        //         if(targetTile == LevelTile.instance.door)
        //         {
        //             if(isPlayerWayOnKey == false)
        //             {
        //                 distance = i;
        //                 return distance - 1;
        //             }
        //         }

        //         LevelElementBase levelElementHit = LevelElementBase.GetAt(targetToCheck);
        //         if(levelElementHit)
        //         {
        //             distance = i;
        //             return distance;
        //         }

        //         if(targetTile == LevelTile.instance.wall)
        //         {
        //             distance = i;
        //             return distance - 1;
        //         }
        //     }
        //     return 0;
        // }
    }
}
