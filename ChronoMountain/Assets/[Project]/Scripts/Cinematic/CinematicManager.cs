using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CinematicManager : MonoBehaviour
{
    [Header("Scene Object To Set :")]
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] Image background;

    [Header("Animation Settings :")]
    [SerializeField] RectTransform rectBackground;
    [SerializeField] float yGap;
    Tween currentTween;

    [Space]
    [SerializeField] List<ScriptableCinematic> cinematicList;
    int cinematicIndex = 0;

    //! Initialise Background / image + text
    public void StartNextCinematic()
    {
        print("StartNextCinematic call !");
        //! set les elements de la scene
        background.sprite = cinematicList[cinematicIndex].backGround;

        DoAnimation(cinematicList[cinematicIndex].animationTime, true);
        StartCoroutine(PrintTexte(cinematicIndex));
        cinematicIndex++;
    }

    //! affiche le texe char par char
    IEnumerator PrintTexte(int index)
    {
        mainText.text = "";
        string textToSet = cinematicList[index].mainText;
        float delais = cinematicList[index].timeToPrintText / textToSet.Length;
        for (int i = 0; i < textToSet.Length; i++)
        {
            mainText.text += textToSet[i];
            yield return new WaitForSeconds(delais);
        }
    }

    //! joue l'animation du fond
    public void DoAnimation(float duration, bool animationDirection)
    {
        float newY;
        currentTween = DOTween.To((time) =>
        {
            if(animationDirection)
                newY = Mathf.Lerp(-yGap, yGap, time);
            else
                newY = Mathf.Lerp(yGap, -yGap, time);

            rectBackground.anchoredPosition = new Vector3(rectBackground.anchoredPosition.x, newY);
        }
        , 0, 1, duration).OnComplete(()=>{DoAnimation(duration, !animationDirection);});
    }

}
