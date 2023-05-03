using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using UnityEngine.Events;

namespace Mwa.Chronomountain
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Tilemap levelTileMap;
        [SerializeField] Sprite wallSprite;
        [SerializeField] Sprite pathSprite;
        [SerializeField] Sprite bumperSprite;
        [SerializeField] float speed;
        [SerializeField] UnityEvent onEndMove;
        [SerializeField] List<ScriptableDirection> directionList = new List<ScriptableDirection>();
        bool isMoving = false;

        //! Verefie la distance entre la position du joueur et le prochain mur
        int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition, Sprite spriteToCheck)
        {
            int distance = 0;
            Tile targetTile = (Tile)levelTileMap.GetTile(levelTileMap.WorldToCell(playerPosition));
            Sprite targetSprite = targetTile.sprite;
            // print("Initial Sprite : " + targetSprite.name);
            for(int i = 0; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)levelTileMap.GetTile(levelTileMap.WorldToCell(targetToCheck));
                targetSprite = targetTile.sprite;

                // print("n+" + i + " sprite = " + targetSprite);
                if(targetSprite == spriteToCheck)
                {
                    distance = i - 1;
                    break;
                }
            }
            // print("Distance whit next wall in " + direction.direction + " = " + distance);
            return distance; 
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition)
        {
            distanceToTravel = DistanceWithNextSprite(direction, playerPosition, wallSprite);
            Vector3 nextTarget = transform.position += direction.GetDirection() * distanceToTravel;
            return nextTarget;
        }

        Vector3 positionToGo;
        Vector3 lastFramePositionToGo;
        Vector3 initialMovementPosition;
        int distanceToTravel = 0;
        int directionIndex = 0;
        float lerpT = 0;

        //! Call par le button Move
        public void GetNextMove()
        {
            // print("DoMove call");
            //! Si le nombre de deplacement est plus grand que le nombre de diection on stop.
            if(directionIndex >= directionList.Count)
            {   
                directionList.Clear();
                directionIndex = 0;
                CanvasManager.manager.ClearArrow();
                onEndMove.Invoke();
                return;
            }

            //! Set les vecteur pour le deplacement du joueur
            initialMovementPosition = transform.position;
            positionToGo = GetNextTarget(directionList[directionIndex], initialMovementPosition);

            isMoving = true;
            CanvasManager.manager.CollorArrow(directionIndex);
        }

        void Update()
        {
            // print("InitialMovePos = " + initialMovementPosition);
            // print("PositionToGo = " + positionToGo);

            if(isMoving == false)
                return;

            // print("lerpT = " + lerpT);
            lerpT += Time.deltaTime * (speed / distanceToTravel);

            //! Quand le lerp est fini, on reset et rapelle DoMove
            if(lerpT >= 1)
            {
                // print("lerpT > 1");
                isMoving = false;
                lerpT = 0;
                directionIndex++; 
                //! recentre le player sur la tile ou il est
                transform.position = positionToGo;

                GetNextMove();
            }

            if(isMoving)
                transform.position = Vector3.Lerp(initialMovementPosition, positionToGo, lerpT);
        }

        Tile GetTileUnderPlayer()
        {
            return (Tile)levelTileMap.GetTile(levelTileMap.WorldToCell(transform.position));
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionList.Add(toAdd);
        }


        //! Call par le reseter
        public void Reseter()
        {
            directionIndex = 0;
            directionList.Clear();
        }
    }
}