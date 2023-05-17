using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelWorldSwitchAnimation : MonoBehaviour
{
    [SerializeField] int numberOfWorld;
    [SerializeField] float swipeThresold;
    [SerializeField] float offSet;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float animationSpeed;
    [SerializeField] AnimationCurve curve;
    int indexMovement;
    Vector2 newPos;
    Vector2 initialPosition;
    Vector2 posToStartMove;
    float lerpT;
    float distance;

    void Start()
    {
        initialPosition = rectTransform.anchoredPosition;
        AddToIndexMovement(0);
    }

    public void SwipCheck(Vector2 swipDirection)
    {
        if(swipDirection.x > swipeThresold)
        {
            AddToIndexMovement(1);
        }
        if(swipDirection.x < -swipeThresold)
        {
            AddToIndexMovement(-1);
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
