using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseOverlayAnimation : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float speed;
    [SerializeField] float newY;
    RectTransform rectTransform;
    Vector2 newPosition;
    Vector2 initialPosition;
    Vector2 target;
    Tween currentTween;
    bool isUp = false;


    void Start()
    {
        rectTransform = (RectTransform)overlay.transform;

        initialPosition = rectTransform.anchoredPosition;
        newPosition = new Vector2(rectTransform.anchoredPosition.x, newY);

        // rectTransform.anchoredPosition = newPosition;
    }

    //! call par button pause
    public void SwitchOverlayState()
    {
        print("call !");
        isUp = !isUp;
        Vector2 currentPosition = rectTransform.anchoredPosition;

        if(isUp == true) //! cas part du haut, va en bas
        {
            target = newPosition;
        }

        if(isUp == false) //! cas par du bas, va en haut
        {
            target = initialPosition;
        }

        // if(currentTween.IsActive())
        // {
        //     currentTween.Kill();
        //     currentTween = null;
        // }

        print("currentPosition = " + currentPosition);
        print("target = " + target);
        currentTween = DOTween.To((time) => 
        {
            rectTransform.anchoredPosition = Vector2.Lerp(currentPosition, target, curve.Evaluate(time));
        },
        0, 1, speed).SetSpeedBased().SetUpdate(true).SetEase(Ease.Linear);
    }
}