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
        public enum MovementState
        {
            moving,
            bumping,
            convoying
        }
        [SerializeField] bool createDebugTarget;
        [SerializeField] GameObject debugTarget;
        [SerializeField] Tilemap levelPathTileMap;
        [SerializeField] Transform playerSpriteTransform;
        [SerializeField] List<ScriptableDirection> directionList = new List<ScriptableDirection>();

        [Header("Movement :")]
        [SerializeField] float speed;
        [SerializeField] float timeToTravel;
        [SerializeField] AudioClip clipOnMovement;
        public UnityEvent onMovementStart;
        [SerializeField] UnityEvent<Tile> onMoveSequenceEnd;
        float speedBackup;

        [Header("Bumping :")]
        [SerializeField][Range(0, 1)] float bumpSpeedFactor;
        [SerializeField] AnimationCurve bumpAnimationCurve;
        [SerializeField] float bumpingRotationSpeed;

        [Header("Convoying :")]
        [SerializeField] float convoyingSpeedFactor;

        float lerpt;
        Tweener currentTween;
        Vector3 positionToGo;
        Vector3 initialMovementPosition;
        int distanceToTravel = 0;
        int directionIndex = 0;
        bool isBumping = false;


        void Start()
        {
            speedBackup = speed;
        }

        //! Call par le button Do Move et onTimerComplete
        public void StartMovement()
        {
            initialMovementPosition = transform.position;
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition), MovementState.moving);
            // GameObject.FindGameObjectWithTag("GameManager").GetComponent<Timer>().PauseTimer();
            onMovementStart.Invoke();
        }

        public void MakeTweenMovement(Vector3 target, MovementState movementType)
        {
            speed = speedBackup;
            //TODO prise en compte des tiles de vide
            switch (movementType)
            {
                //TODO voire avec une curve pour jouer sur un effet d'acceleration
                //! Slide movement
                case MovementState.moving :
                    print("MovementState.moving");
                    SetRotation(directionList[directionIndex]);
                    InGameCanvasManager.manager.CollorArrow(directionIndex);
                    currentTween = DOTween.To((lerpT) =>
                    {
                        transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
                    },
                    0, 1, speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
                    directionIndex++;
                break;

                //! Bumping Movement
                case MovementState.bumping :
                    currentTween = DOTween.To((lerpT) =>
                    {
                        transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
                        transform.localScale = Vector3.one * bumpAnimationCurve.Evaluate(lerpT);
                        transform.Rotate(new Vector3(0, 0, bumpingRotationSpeed * Time.deltaTime));
                    },
                    0, 1, speed * bumpSpeedFactor).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
                break;

                case MovementState.convoying :
                    print("MovementState.convoying");
                    currentTween = DOTween.To((lerpT) =>
                    {
                        transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
                        transform.Rotate(new Vector3(0, 0, bumpingRotationSpeed * Time.deltaTime));
                    },
                    0, 1, speed * convoyingSpeedFactor).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
                break;
            }
        }

        public void TweenComplete()
        {
            isBumping = false;
            if(directionIndex >= directionList.Count)
            {
                //! Fin de la sequence de movement, envoi la tile sous le joueur pour etre verifier dans EndLevelCheck
                onMoveSequenceEnd.Invoke(GetTileUnderPlayer());
            }
            else
            {
                //! Relance avec une nouvelle target
                initialMovementPosition = transform.position;
                MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition), MovementState.moving);
            }
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition)
        {
            //TODO Mettre l'invok du son autre par
            //! Joue un son a chaque changement de target
            AudioManager.manager.PlaySfx(clipOnMovement);

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
                    break;
                }
            }

            // print("Distance to travel = " + distance);
            return distance; 
        }


        //! Set les parametre pour addapter l'update au level element rencontrer
        public void SetLevelElement(LevelElement levelElement, Vector3 levelElementPosition)
        {
            if(levelElement.type == LevelElementType.bumper)
            {
                print("LevelElementType.bumper");
                currentTween.Kill();
                isBumping = true;
                transform.position = levelElementPosition;
                initialMovementPosition = levelElementPosition;
                MakeTweenMovement(levelElement.target.position, MovementState.bumping);
            }
            
            if(levelElement.type == LevelElementType.conveyor && isBumping == false)
            {
                print("LevelElementType.conveyor");
                currentTween.Kill();
                transform.position = levelElementPosition;
                initialMovementPosition = transform.position;
                MakeTweenMovement(GetNextTarget(levelElement.direction, initialMovementPosition), MovementState.convoying);
            }
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionList.Add(toAdd);
        }

        //! position du joueur reset dans LevelReseter
        public void ResetMovement()
        {
            directionIndex = 0;
            directionList.Clear();
            transform.localScale = Vector3.one;
        }

        void SetRotation(ScriptableDirection direction)
        {
            //! direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction
            switch(direction.direction)
            {
                case Pointer.Up :
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;

                case Pointer.Left :
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;

                case Pointer.Right :
                    playerSpriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;

                case Pointer.Down :
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