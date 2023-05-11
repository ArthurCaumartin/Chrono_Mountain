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
        [SerializeField] bool createDebugTarget;
        [SerializeField] GameObject debugTarget;
        [SerializeField] Tilemap levelPathTileMap;
        [SerializeField] Transform playerSpriteTransform;
        [SerializeField] List<ScriptableDirection> directionList = new List<ScriptableDirection>();
        [Header("Movement :")]
        [SerializeField] float speed;
        public UnityEvent onMovementStart;
        [SerializeField] UnityEvent<Tile> onMoveSequenceEnd;
        float speedBackup;

        [Header("Bumping :")]
        [SerializeField][Range(0, 1)] float bumpSpeedFactor;
        [SerializeField] AnimationCurve bumpAnimationCurve;

        [Header("Falling :")]
        [SerializeField] float fallingRotationSpeed;
        [SerializeField] float scaleChangeSpeed;

        bool isAlreadyFalling = false;
        bool isMoving = false;
        bool isBumping = false;
        bool isFalling = false;
        Vector3 positionToGo;
        Vector3 initialMovementPosition;
        int distanceToTravel = 0;
        int directionIndex = 0;
        float lerpT = 0;

        void Start()
        {
            speedBackup = speed;
        }

        //! Call par le button Do Move et onTimerComplete
        public void StartMovement()
        {
            print("Start Move");
            GetNextMove();
            // GameObject.FindGameObjectWithTag("GameManager").GetComponent<Timer>().PauseTimer();
            onMovementStart.Invoke();
        }

        [ContextMenu("GetNextMove")]
        public void GetNextMove()
        {
            print("Get Next Move");
            //! Variable reset qui on étais changer pour la bumper
            speed = speedBackup;

            //! Si le nombre de deplacement est plus grand que le nombre de diection on stop.
            if(directionIndex >= directionList.Count)
            {   
                EndMovementSequence();
                return;
            }

            //! Set les vecteur pour le deplacement du joueur
            initialMovementPosition = transform.position;
            SetRotation(directionList[directionIndex]);
            positionToGo = GetNextTarget(directionList[directionIndex], initialMovementPosition);
            isMoving = true;
            isBumping = false;
            InGameCanvasManager.manager.CollorArrow(directionIndex);
        }

        void EndMovementSequence()
        {
            // directionList.Clear();
            // directionIndex = 0;
            // InGameCanvasManager.manager.ClearArrow();

            onMoveSequenceEnd.Invoke(GetTileUnderPlayer());
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition)
        {
            distanceToTravel = DistanceWithNextSprite(direction, playerPosition, LevelSprite.manager.wall);
            Vector3 nextTarget = transform.position + direction.GetDirection() * distanceToTravel;

            if(createDebugTarget)
            {
                Instantiate(debugTarget, nextTarget, Quaternion.identity);
            }


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
                    // print(distance);
                    break;
                }
            }

            // print("Distance to travel = " + distance);
            return distance; 
        }

        void Update()
        {
            if(isMoving)
            {
                lerpT += Time.deltaTime * (speed / distanceToTravel);
            }

            if(isFalling == true || isBumping == true)
            {
                //! Rotate player pour la chute, scale changé par un tween dans Falling()
                transform.Rotate(new Vector3(0, 0, fallingRotationSpeed * Time.deltaTime));
            }

            if(isBumping == true)
            {
                transform.localScale = Vector3.one * bumpAnimationCurve.Evaluate(lerpT);
            }

            //! Quand le lerp est fini, on reset et rapelle DoMove
            if(lerpT >= 1f)
            {
                isMoving = false;
                lerpT = 0;
                directionIndex++; 
                //! recentre le player sur la tile ou il est
                transform.position = positionToGo;
                transform.localScale = Vector3.one;
                GetNextMove();
            }

            //? Pas sur de comprendre pourquoi l'ordre d'execution import autant
            if(isMoving)
            {
                transform.position = Vector3.Lerp(initialMovementPosition, positionToGo, lerpT);
            }

            //? Pas sur de comprendre pourquoi l'ordre d'execution import autant
            if(GetTileUnderPlayer().sprite == LevelSprite.manager.hole)
            {
                if(isBumping == false && isAlreadyFalling == false)
                {
                    Falling();
                }
            }
        }

        void Falling()
        {
            isMoving = false;
            isFalling = true;
            isAlreadyFalling = true;
            transform.DOScale(Vector3.zero, scaleChangeSpeed).SetEase(Ease.InOutElastic).SetSpeedBased().OnComplete(EndMovementSequence);
        }

        //! Set les parametre pour addapter l'update au level element rencontrer
        public void SetLevelElement(LevelElement levelElement, Vector3 levelElementPosition)
        {
            // print("SetLevelElement Call !");
            if(levelElement.type == LevelElementType.bumper)
            {
                lerpT = 0;
                speed *= bumpSpeedFactor;
                isMoving = true;
                isBumping = true;
                transform.position = levelElementPosition;
                initialMovementPosition = levelElementPosition;
                positionToGo = levelElement.target.position;
            }
            
            if(levelElement.type == LevelElementType.conveyor && isBumping == false)
            {
                lerpT = 0;
                isBumping = false;
                isMoving = true;
                transform.position = levelElementPosition;
                initialMovementPosition = transform.position;
                SetRotation(levelElement.direction);
                positionToGo = GetNextTarget(levelElement.direction, initialMovementPosition);
            }
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionList.Add(toAdd);
        }

        public void ResetMovement()
        {
            directionIndex = 0;
            directionList.Clear();
            transform.localScale = Vector3.one;

            isFalling = false;
            isBumping = false;
            isMoving = false;
            isAlreadyFalling = false;
            lerpT = 0;
        }

        void SetRotation(ScriptableDirection direction)
        {
                     //! direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction
            switch(direction.direction)
            {
                case Pointer.Up :
                    print(Pointer.Up);
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;

                case Pointer.Left :
                    print(Pointer.Left);
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;

                case Pointer.Right :
                    print(Pointer.Right);
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;

                case Pointer.Down :
                    print(Pointer.Down);
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
            }
        }

        Tile GetTileUnderPlayer()
        {
            return (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(transform.position));
        }
    }
}