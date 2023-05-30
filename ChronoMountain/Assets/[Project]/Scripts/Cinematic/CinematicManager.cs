using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CinematicManager : MonoBehaviour
{//! c pa vraiment une cinematic en faite... ?
    [Header("Scene Object To Set :")]
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] Image background;
    [SerializeField] Image blackImage;
    [SerializeField] Button skipButton;

    [Header("Animation Settings :")]
    [SerializeField] RectTransform rectBackground;
    [SerializeField] float yGap;
    [SerializeField] float blackFadeTime;
    Tween backgroundTween;
    Coroutine printTextCoroutine;

    [Space]
    [Header("Events :")]
    [SerializeField] UnityEvent onCinematiqueEnd;
    [Space]

    [SerializeField] List<ScriptableCinematic> cinematicList;
    int cinematicIndex = 0;

    void Start()
    {
        background.sprite = cinematicList[0].backGroundSprite;
        FadeBlackTransition(1, 0, () => {SetCinematic(0);});
        cinematicIndex++;
    }

    public void NextCinematic()
    {
        print("Start New Cinematique !");
        if(cinematicIndex > cinematicList.Count)
        {
            print("Cinematique Sequence End !");
            FadeBlackTransition(0, 1, () => {onCinematiqueEnd.Invoke();});
            return;
        }

        SetCinematic(cinematicIndex);
        cinematicIndex++;
    }

    public void SkipDialogue()
    {

    }

    //! Initialise Background / image + text
    void SetCinematic(int index)
    {
        //! set les elements de la scene
        background.sprite = cinematicList[index].backGroundSprite;

        DoBackgroundAnimation(cinematicList[index].backgroundMoveDuration, true);
        printTextCoroutine = StartCoroutine(PrintTexte(index));
        index++;
    }

    //! affiche le texe char par char
    IEnumerator PrintTexte(int texeIndex) //TODO devrait prendre un string !
    {
        mainText.text = "";
        string textToSet = cinematicList[texeIndex].mainText;
        float delais = cinematicList[texeIndex].timeToPrintText / textToSet.Length;
        for (int i = 0; i < textToSet.Length; i++)
        {
            mainText.text += textToSet[i];
            yield return new WaitForSeconds(delais);
        }
    }


    //! joue l'animation du fond
    void DoBackgroundAnimation(float duration, bool animationDirection)
    {
        float newY;
        backgroundTween = DOTween.To((time) =>
        {
            if(animationDirection)
                newY = Mathf.Lerp(-yGap, yGap, time);
            else
                newY = Mathf.Lerp(yGap, -yGap, time);

            rectBackground.anchoredPosition = new Vector3(rectBackground.anchoredPosition.x, newY);
        }
        , 0, 1, duration).OnComplete(()=>{DoBackgroundAnimation(duration, !animationDirection);});
    }

    void FadeBlackTransition(int startAlpha, int endAlpha, System.Action callback)
    {
        DOTween.To((time) =>
        {
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, time);
        }
        , startAlpha, endAlpha, blackFadeTime).OnComplete(() => callback());
    }
}
