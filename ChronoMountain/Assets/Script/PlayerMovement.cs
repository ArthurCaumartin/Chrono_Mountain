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

        [SerializeField] bool createDebugTarget;
        [SerializeField] GameObject debugTarget;
        [SerializeField] Tilemap levelPathTileMap;
        [SerializeField] Transform playerSpriteTransform;
        [SerializeField] List<ScriptableDirection> directionList = new List<ScriptableDirection>();

        [Header("Movement :")]
        public float speed;
        float speedBackup;
        [SerializeField] float timeToTravel;
        [SerializeField] AudioClip clipOnMovement;
        public UnityEvent onMovementStart;
        [SerializeField] UnityEvent<Tile> onMoveSequenceEnd;

        [Header("Convoying :")]
        Tweener currentTween;
        Vector3 initialMovementPosition;
        int distanceToTravel = 0;
        int directionIndex = 0;
        // LevelElementBase levelElementBase;

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
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition, out nextLevelElement), nextLevelElement);
            initialMovementPosition = transform.position;
            onMovementStart.Invoke();
        }

        //? entre le this. et le out je suis un peut perdu pour savoir qui est "levelElementBase" ou "nextLevelElement"

        public void MakeTweenMovement(Vector3 target, LevelElementBase levelElementBase)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Timer>().PauseTimer();
            // this.levelElementBase = levelElementBase;
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

        LevelElementBase levelElementUnderPlayer;

        //TODO Voire l'enchainement pour 
        public void TweenComplete()
        {
            levelElementUnderPlayer = null;
            // print("directionList.Count : " + directionList.Count);
            // print("directionIndex : " + directionIndex);
            levelElementUnderPlayer = LevelElementBase.GetAt(transform.position);
            print(levelElementUnderPlayer);
            if(directionIndex >= directionList.Count)
            {
                if(levelElementUnderPlayer)
                {
                    print("level element exist lance OnStep from :" + levelElementUnderPlayer.name);
                    levelElementUnderPlayer.OnStep(TweenComplete);
                }
                else
                {
                    // print("End Movement Sequence !");
                    onMoveSequenceEnd.Invoke(GetTileUnderPlayer());
                }
            }
            else
            {
                if(levelElementUnderPlayer)
                {
                    levelElementUnderPlayer.OnStep(TweenComplete);
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
            MakeTweenMovement(GetNextTarget(directionList[directionIndex], initialMovementPosition, out nextLevelElement), nextLevelElement);
        }

        Vector3 GetNextTarget(ScriptableDirection direction, Vector3 playerPosition, out LevelElementBase nextLevelElement)
        {
            // print("GetNextTarget");
            AudioManager.manager.PlaySfx(clipOnMovement);

            distanceToTravel = TileDistance.instance.DistanceWithNextSprite(direction, playerPosition, out nextLevelElement);

            Vector3 nextTarget = transform.position + (direction.GetDirection() * distanceToTravel);
            return nextTarget;
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionList.Add(toAdd);
        }

        //! position du joueur reset dans LevelReseter
        public void ResetMovement()
        {
            directionList.Clear();
            directionIndex = 0;
            currentTween.Kill();
            levelElementUnderPlayer.KillTween();
            
            transform.localScale = Vector3.one;
        }

        void SetRotation(ScriptableDirection direction)
        {
            //! direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction.direction
            switch(direction.pointer)
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