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
        LevelElementBase levelElementBase;

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
            LevelElementBase nextLevelElement; //! Set par le out de MakeTweenMovement !
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition, out nextLevelElement), MovementState.moving, nextLevelElement);
            initialMovementPosition = transform.position;
            onMovementStart.Invoke();
        }
        //? entre le this. et le out je suis un peut perdu pour savoir qui est "nextLevelElement"
        public void MakeTweenMovement(Vector3 target, MovementState movementType, LevelElementBase levelElementBase)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Timer>().PauseTimer();
            this.levelElementBase = levelElementBase;
            speed = speedBackup;
            
            SetRotation(directionList[directionIndex]);
            InGameCanvasManager.manager.CollorArrow(directionIndex);

            currentTween = DOTween.To((lerpT) => //! lerpT == la valeur qui varie de 0 a 1 (je sais pas comment :))
            {
                transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
            },
            0, 1, speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(TweenComplete);
            directionIndex++;
        }

        public void TweenComplete()
        {
            if(directionIndex >= directionList.Count)
            {
                print("End Movement Sequence !");
                onMoveSequenceEnd.Invoke(GetTileUnderPlayer());
            }
            else
            {
                if(levelElementBase)
                {
                    levelElementBase.OnStep(ChainNextMove);
                }
                else
                {
                    ChainNextMove();
                }
            }
        }

        void ChainNextMove()
        {
            LevelElementBase nextLevelElement;
            initialMovementPosition = transform.position;
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition, out nextLevelElement), MovementState.moving, nextLevelElement);
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition, out LevelElementBase nextLevelElement)
        {
            print("GetNextTarget");
            AudioManager.manager.PlaySfx(clipOnMovement);

            Vector3 nextTarget;

            distanceToTravel = DistanceWithNextSprite(direction, playerPosition, out nextLevelElement);
            if(nextLevelElement)
            {
                nextTarget = transform.position + direction.GetDirection() * distanceToTravel;
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
                // print(targetTile);

                if(createDebugTarget && Application.isEditor)
                    Instantiate(debugTarget, targetToCheck, Quaternion.identity);

                LevelElementBase le = LevelElementBase.GetAt(targetToCheck);
                if(le) {
                    levelElementTile = le;
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