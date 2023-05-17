using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelWorldSwitchAnimation : MonoBehaviour
{
    public int numberOfWorld;
    public float offSet;
    public RectTransform rectTransform;
    public float animationSpeed;
    public AnimationCurve curve;
    int indexMovement;
    Vector2 newPos;
    Vector2 initialPosition;
    Vector2 posToStartMove;
    float lerpT;
    float distance;
    public SwipeDetect levelSwiper;

    void Start()
    {
        initialPosition = rectTransform.anchoredPosition;
        AddToIndexMovement(0);
        levelSwiper.onSwipe.AddListener(OnSwipe);
    }
    
        
    public void OnSwipe(Vector2 direction) {
        Debug.Log("SwipeDetect : " + direction);
        if(direction.x > .5f) {
            AddToIndexMovement(1);
        }
        else if(direction.x < -.5f) {
            AddToIndexMovement(-1);
        } else {
            Debug.Log("SwipeDetect up or down : " + direction);
        }
    }

    void Update()
    {
        if(lerpT < 1)
        {
            lerpT += Time.deltaTime * animationSpeed;
            newPos = new Vector2(initialPosition.x + offSet * indexMovement, initialPosition.y);
            rectTransform.anchoredPosition = Vector2.LerpUnclamped(posToStartMove, newPos, curve.Evaluate(lerpT));
        }
    }

    public void AddToIndexMovement(int toAdd)
    {
        if(indexMovement + toAdd > 0 || indexMovement + toAdd < -numberOfWorld + 1)
            return;

        indexMovement += toAdd;
        posToStartMove = rectTransform.anchoredPosition;


        lerpT = 0;
    }
}
