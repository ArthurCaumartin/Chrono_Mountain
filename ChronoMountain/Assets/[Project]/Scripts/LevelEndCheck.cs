using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

namespace Mwa.Chronomountain
{
    public class LevelEndCheck : MonoBehaviour
    {
        [SerializeField] bool loadScene;
        [SerializeField] string sceneToLoadAtEnd;

        [Header("Win Animation :")]
        [SerializeField] float waitDuration;
        [SerializeField] float winScaleFactor;
        [Header("Lose Animation :")]
        [SerializeField] Sprite loseSprite;
        [SerializeField] float loseScaleFactor;
        [Space]
        [SerializeField] ScriptableLevel level;
        bool isWin = false;
        public void TileCheck(Tile underPlayerTile)
        {
            // print("Tile Check call !");
            if(underPlayerTile == LevelTile.instance.end)
                isWin = true;
            else
                isWin = false;

            // print("isWin = " + isWin); 
            StartCoroutine(EndLevel(isWin, waitDuration));
        }

        IEnumerator EndLevel(bool isWin, float waitTime)
        {
            // print("Coroutine Start");
            if(isWin)
            {
                transform.DOScale(Vector3.one * winScaleFactor, waitTime/2).SetEase(Ease.InBounce)
                .OnComplete(()=> 
                {
                    transform.DOScale(Vector3.one, waitTime/2).SetEase(Ease.InBounce);
                });

                level.SetIsWin(true);

                yield return new WaitForSeconds(waitTime);

                if(loadScene)
                {
                    InGameCanvasManager.manager.gameObject.SetActive(false);
                    SceneLoader.instance.LoadScene(sceneToLoadAtEnd);
                }

                InGameCanvasManager.manager.SetWin();
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().sprite = loseSprite;

                transform.DOScale(Vector3.one * loseScaleFactor, waitTime).SetEase(Ease.Linear);

                yield return new WaitForSeconds(waitTime);
                InGameCanvasManager.manager.SetLose();
            }
        }
    }
}