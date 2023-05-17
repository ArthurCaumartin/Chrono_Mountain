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
        public static PlayerMovement instance;
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
        float speedBackup;
        [SerializeField] float timeToTravel;
        [SerializeField] AudioClip clipOnMovement;
        public UnityEvent onMovementStart;
        [SerializeField] UnityEvent<Tile> onMoveSequenceEnd;

        [Header("Bumping :")]
        [SerializeField][Range(0, 1)] float bumpSpeedFactor;
        [SerializeField] AnimationCurve bumpAnimationCurve;
        [SerializeField] float bumpingRotationSpeed;

        [Header("Convoying :")]
        [SerializeField] float convoyingSpeedFactor;

        LevelElementType nextElement;
        Tweener currentTween;
        Vector3 initialMovementPosition;
        int distanceToTravel = 0;
        int directionIndex = 0;
        bool isBumping = false;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            speedBackup = speed;
        }

        //! Call par le button Do Move et onTimerComplete
        public void StartMovement()
        {
            //! si pas d'input entrer par le joueur, alors on call onMoveSequenceEnd et return
            if(directionList.Count == 0)
            {
                onMoveSequenceEnd.Invoke(GetTileUnderPlayer());
                return;
            }
            initialMovementPosition = transform.position;
            LevelElementBase nextLevelElement;
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition, out nextLevelElement), MovementState.moving, nextLevelElement);
            initialMovementPosition = transform.position;
            onMovementStart.Invoke();
        }

        public void MakeTweenMovement(Vector3 target, MovementState movementType, LevelElementBase levelElementBase)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Timer>().PauseTimer();
            this.levelElementBase = levelElementBase;
            speed = speedBackup;
            
            print("MovementState.moving");
            SetRotation(directionList[directionIndex]);
            InGameCanvasManager.manager.CollorArrow(directionIndex);
            currentTween = DOTween.To((lerpT) =>
            {
                transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
            },
            0, 1, speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
            directionIndex++;
            //TODO prise en compte des tiles de vide
            // switch (movementType)
            // {
            //     //TODO voire avec une curve pour jouer sur un effet d'acceleration
            //     //! Slide movement
            //     case MovementState.moving :
            //         print("MovementState.moving");
            //         SetRotation(directionList[directionIndex]);
            //         InGameCanvasManager.manager.CollorArrow(directionIndex);
            //         currentTween = DOTween.To((lerpT) =>
            //         {
            //             transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
            //         },
            //         0, 1, speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
            //         directionIndex++;
            //     break;

            //     //! Bumping Movement
            //     case MovementState.bumping :
            //         currentTween = DOTween.To((lerpT) =>
            //         {
            //             transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
            //             transform.localScale = Vector3.one * bumpAnimationCurve.Evaluate(lerpT);
            //             transform.Rotate(new Vector3(0, 0, bumpingRotationSpeed * Time.deltaTime));
            //         },
            //         0, 1, speed * bumpSpeedFactor).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
            //     break;

            //     case MovementState.convoying :
            //         print("MovementState.convoying");
            //         currentTween = DOTween.To((lerpT) =>
            //         {
            //             transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
            //             transform.Rotate(new Vector3(0, 0, bumpingRotationSpeed * Time.deltaTime));
            //         },
            //         0, 1, speed * convoyingSpeedFactor).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
            //     break;
            // }
        }
        LevelElementBase levelElementBase;
        public void TweenComplete()
        {
            isBumping = false;

            if(directionIndex >= directionList.Count)
            {
                print("directionIndex : " + directionIndex);
                print("directionList.Count : " + directionList.Count);
                //! Fin de la sequence de movement, envoi la tile sous le joueur pour etre verifier dans EndLevelCheck
                onMoveSequenceEnd.Invoke(GetTileUnderPlayer());
            }
            else
            {
                //! Relance avec une nouvelle target
                if(levelElementBase) {
                    levelElementBase.OnStep(ChainNextMove);
                } else {
                    ChainNextMove();
                }
            }
        }

        void ChainNextMove() {
            LevelElementBase nextLevelElement;
            initialMovementPosition = transform.position;
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition, out nextLevelElement), MovementState.moving, nextLevelElement);
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition, out LevelElementBase nextLevelElement)
        {
            //TODO Mettre l'invok du son autre par
            //! Joue un son a chaque changement de target
            AudioManager.manager.PlaySfx(clipOnMovement);

            Vector3 nextTarget;
            Vector3 directionToSetTarget;

            
            distanceToTravel = DistanceWithNextSprite(direction, playerPosition, out nextLevelElement);
            if(nextLevelElement)
            {
                //! gere le cas des convoyeur
                // if(nextLevelElement == global::LevelTile.instance.coveyorUp)
                //     directionToSetTarget = Vector3.up;
                // if(nextLevelElement == global::LevelTile.instance.coveyorRight)
                //     directionToSetTarget = Vector3.right;
                // if(nextLevelElement == global::LevelTile.instance.coveyorDown)
                //     directionToSetTarget = Vector3.down;
                // if(nextLevelElement == global::LevelTile.instance.coveyorLeft)
                //     directionToSetTarget = Vector3.left;
                
                nextTarget = transform.position + direction.GetDirection() * distanceToTravel;

                // // lancer un tween
                // nextLevelElement.OnStep();
            }
            else
            {
                nextTarget = transform.position + direction.GetDirection() * distanceToTravel;
            }
            return nextTarget; 
        }

        int DistanceWithNextSprite(ScriptableDirection direction, Vector3 playerPosition, out LevelElementBase levelElementTile)
        {//* le gro bordel
            int distance = 0;
            TileBase targetTile = levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(playerPosition));

            for(int i = 1; i < 100; i++) 
            {
                Vector3 targetToCheck = playerPosition + (direction.GetDirection() * i);
                targetTile = (Tile)levelPathTileMap.GetTile(levelPathTileMap.WorldToCell(targetToCheck));
                print(targetTile);

                if(createDebugTarget && Application.isEditor)
                    Instantiate(debugTarget, targetToCheck, Quaternion.identity);

                // if(targetTile && global::LevelTile.instance.levelElementTile.Contains(targetTile))
                // {
                //     levelElementTile = targetTile;
                //     distance = i;
                //     return distance;
                // }

                LevelElementBase le = LevelElementBase.GetAt(targetToCheck);
                if(le) {
                    levelElementTile = le;
                    distance = i;
                    return distance;
                }

                if(targetTile == global::LevelTile.instance.wall)
                {
                    levelElementTile = null;
                    distance = i - 1;
                    return distance - 1;
                }
            }
            levelElementTile = null;
            return 0;
        }
        
        public void SetLevelElement(LevelElement levelElement, Vector3 levelElementPosition)
        {
            // if(levelElement.type == LevelElementType.bumper)
            // {
            //     print("LevelElementType.bumper");
            //     currentTween.Kill();
            //     isBumping = true;
            //     transform.position = levelElementPosition;
            //     initialMovementPosition = levelElementPosition;
            //     MakeTweenMovement(levelElement.target.position, MovementState.bumping);
            // }
            
            // if(levelElement.type == LevelElementType.conveyor && isBumping == false)
            // {
            //     print("LevelElementType.conveyor");
            //     currentTween.Kill();
            //     transform.position = levelElementPosition;
            //     initialMovementPosition = transform.position;
            //     MakeTweenMovement(GetNextTarget(levelElement.direction, initialMovementPosition), MovementState.convoying);
            // }
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