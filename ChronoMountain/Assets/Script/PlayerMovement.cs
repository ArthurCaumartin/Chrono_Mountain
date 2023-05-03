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
        [SerializeField] Tilemap levelPathTileMap;
        [SerializeField] UnityEvent onEndMove;
        [SerializeField] List<ScriptableDirection> directionList = new List<ScriptableDirection>();
        [Header("Movement :")]
        [SerializeField] float speed;
        float speedBackup;

        [Header("Bumping :")]
        [SerializeField][Range(0, 1)] float bumpSpeedFactor;
        [SerializeField] AnimationCurve bumpAnimationCurve;
        [SerializeField] float scaleOffset;

        Tilemap levelElementTileMap;
        bool isMoving = false;
        bool isBumping = false;
        Vector3 positionToGo;
        Vector3 lastFramePositionToGo;
        Vector3 initialMovementPosition;
        int distanceToTravel = 0;
        int directionIndex = 0;
        float lerpT = 0;

        void Start()
        {
            speedBackup = speed;
        }

        public void GetNextMove()
        {
            //! Variable reset qui on étais changer pour la bumper
            GetComponent<Collider2D>().enabled = true;
            speed = speedBackup;

            //! Si le nombre de deplacement est plus grand que le nombre de diection on stop.
            if(directionIndex >= directionList.Count)
            {   
                ResetMovement();
                return;
            }

            //! Set les vecteur pour le deplacement du joueur
            initialMovementPosition = transform.position;
            positionToGo = GetNextTarget(directionList[directionIndex], initialMovementPosition);
            isMoving = true;
            isBumping = false;
            CanvasManager.manager.CollorArrow(directionIndex);
        }

        void ResetMovement()
        {
            directionList.Clear();
            directionIndex = 0;
            CanvasManager.manager.ClearArrow();
            onEndMove.Invoke();
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition)
        {
            distanceToTravel = DistanceWithNextSprite(direction, playerPosition, LevelSprite.manager.wall);
            Vector3 nextTarget = transform.position += direction.GetDirection() * distanceToTravel;
            return nextTarget;
        }

        int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition, Sprite spriteToCheck)
        {
            int distance = 0;
            Tile targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));
            Sprite targetSprite = targetTile.sprite;

            for(int i = 0; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
                targetSprite = targetTile.sprite;
                if(targetSprite == spriteToCheck)
                {
                    distance = i - 1;
                    break;
                }
            }
            return distance; 
        }

        public Vector3 newScale;

        void Update()
        {
            if(isMoving)
            {
                lerpT += Time.deltaTime * (speed / distanceToTravel);
            }

            if(isBumping == true)
            {
                // scaleOffset *= bumpAnimationCurve.Evaluate(lerpT);
                // newScale = new Vector3(1 + scaleOffset, 1 + scaleOffset, 0);
                transform.localScale = Vector3.one * bumpAnimationCurve.Evaluate(lerpT);
            }

            //! Quand le lerp est fini, on reset et rapelle DoMove
            if(lerpT >= 1)
            {
                isMoving = false;
                lerpT = 0;
                directionIndex++; 
                //! recentre le player sur la tile ou il est
                transform.position = positionToGo;
                transform.localScale = Vector3.one;
                GetNextMove();
            }

            if(isMoving)
            {
                transform.position = Vector3.Lerp(initialMovementPosition, positionToGo, lerpT);
            }
        }

        public void SetLevelElement(LevelElement levelElement, Vector3 levelElementPosition)
        {
            print("SetLevelElement Call !");
            if(levelElement.type == LevelElementType.bumper)
            {
                GetComponent<Collider2D>().enabled = false;
                speed *= bumpSpeedFactor;
                isMoving = true;
                isBumping = true;
                lerpT = 0;
                transform.position = levelElementPosition;
                initialMovementPosition = levelElementPosition;
                positionToGo = levelElement.target.position;
            }
            
            if(levelElement.type == LevelElementType.conveyor)
            {
                isMoving = true;
                lerpT = 0;
                transform.position = levelElementPosition;
                initialMovementPosition = transform.position;
                positionToGo = GetNextTarget(levelElement.direction, initialMovementPosition);
            }
        }


        Tile GetTileUnderPlayer()
        {
            return (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(transform.position));
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionList.Add(toAdd);
        }


        public void Reseter()
        {
            directionIndex = 0;
            directionList.Clear();
        }
    }
}