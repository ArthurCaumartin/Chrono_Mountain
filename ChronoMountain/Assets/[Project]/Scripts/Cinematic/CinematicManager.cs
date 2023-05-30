using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CinematicManager : MonoBehaviour
{//! c pa vraiment une cinematic en faite... ?
    [SerializeField] ScriptableCinematic cinematic;
    [Header("Scene Object To Set :")]
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] Image background;
    [SerializeField] Image blackImage;


    [Header("Background Settings :")]
    [SerializeField] RectTransform rectBackground;
    [SerializeField] float animationSpeed;
    [SerializeField] float yGap;
    [SerializeField] float backgroundAnimationTime;
    [SerializeField] float blackFadeTime;

    [Header("Text settings :")]
    [SerializeField] float timeToPrintText;

    [Header("Events :")]
    [SerializeField] UnityEvent onCinematiqueEnd;

    Coroutine printTextCoroutine;
    Tween fadeBlackTween;
    int index = 0;
    int limiteIndex;
    float backgroundLerpT;
    bool animationDirection;
    float newY;

    void Start()
    {
        limiteIndex = cinematic.backGroundSprite.Count;
        // print("Limite index = " + limiteIndex);
        background.sprite = cinematic.backGroundSprite[0];

        FadeBlackTransition(1, 0, SetCinematic);
    }

    public void Button()
    {
        // print("Button clic !");
        if(fadeBlackTween != null && fadeBlackTween.active)
        {
            fadeBlackTween.Kill();
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, 0);
        }

        if(index >= limiteIndex) 
        {
            FadeBlackTransition(0, 1, () => {onCinematiqueEnd.Invoke();});
            return;
        }
        else
        {
            SetCinematic();
        }
    }

    //! Set le background et lance la corouinte print
    void SetCinematic()
    {
        if(printTextCoroutine != null)
            StopCoroutine(printTextCoroutine);

        // print("Parametre index = " + index);
        background.sprite = cinematic.backGroundSprite[index];
        printTextCoroutine = StartCoroutine(PrintTexte(cinematic.textList[index]));
        index++;
    }

    IEnumerator PrintTexte(string toPrint)
    {
        mainText.text = "";
        float delais =  timeToPrintText / toPrint.Length;
        //TODO ajouter un petit delais ici ?
        for (int i = 0; i < toPrint.Length; i++)
        {
            mainText.text = toPrint.Substring(0, i);
            yield return new WaitForSeconds(delais);
        }
    }

    void FadeBlackTransition(int startAlpha, int endAlpha, System.Action callback)
    {
        fadeBlackTween = DOTween.To((time) =>
        {
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, time);
        }
        , startAlpha, endAlpha, blackFadeTime).OnComplete(() => 
        {
            // print("Fade callback !");
            callback();
        });
    }

    //TODO ajouter une courbe d'animation
    void Update()
    {
        backgroundLerpT += Time.deltaTime * animationSpeed;

        if(animationDirection)
            newY = Mathf.Lerp(-yGap, yGap, backgroundLerpT);
        else
            newY = Mathf.Lerp(yGap, -yGap, backgroundLerpT);
        
        rectBackground.anchoredPosition = new Vector3(rectBackground.anchoredPosition.x, newY);

        if(backgroundLerpT > 1)
        {
            backgroundLerpT = 0;
            animationDirection = !animationDirection;
        }
    }

    // void DoBackgroundAnimation(float duration, bool animationDirection)
    // {
    //     float newY;
    //     DOTween.To((time) =>
    //     {
    //         if(animationDirection)
    //             newY = Mathf.Lerp(-yGap, yGap, time);
    //         else
    //             newY = Mathf.Lerp(yGap, -yGap, time);

    //         rectBackground.anchoredPosition = new Vector3(rectBackground.anchoredPosition.x, newY);
    //     }
    //     , 0, 1, duration).OnComplete(()=>{DoBackgroundAnimation(duration, !animationDirection);});
    // }
}
