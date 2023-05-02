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
        [SerializeField] Tilemap tileMap;
        [SerializeField] Sprite wall;
        [SerializeField] float speed;
        [SerializeField] UnityEvent onEndMove;
        [SerializeField] List<ScriptableDirection> directionList = new List<ScriptableDirection>();
        Sprite targetSprite;
        Tile targetTile;
        bool isMoving = false;

        //! Verefie la distance entre la position du joueur et le prochain mur
        int DistanceWithWall(ScriptableDirection direction, Vector3 playerPosition)
        {
            int distance = 0;
            targetTile = (Tile)tileMap.GetTile(tileMap.WorldToCell(playerPosition));
            targetSprite = targetTile.sprite;
            // print("Initial Sprite : " + targetSprite.name);
            for(int i = 0; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)tileMap.GetTile(tileMap.WorldToCell(targetToCheck));
                targetSprite = targetTile.sprite;

                // print("n+" + i + " sprite = " + targetSprite);
                if(targetSprite == wall)
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
            distanceToTravel = DistanceWithWall(direction, playerPosition);
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
            CenterPlayerOnTile();

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
                transform.position = positionToGo;
                GetNextMove();
            }

            if(isMoving)
                transform.position = Vector3.Lerp(initialMovementPosition, positionToGo, lerpT);
        }

        //TODO Fonction pour recentrer le joueur sur sa tile
        void CenterPlayerOnTile()
        {
            // Vector3 newPos = tileMap.CellToWorld(tileMap.WorldToCell(transform.position));
            // transform.position = Mathf.rou
            // print("player recenter");

        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionList.Add(toAdd);
        }
    }
}