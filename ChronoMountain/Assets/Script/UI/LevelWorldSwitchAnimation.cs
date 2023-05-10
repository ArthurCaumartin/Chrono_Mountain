using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelWorldSwitchAnimation : MonoBehaviour
{
    public int numberOfWorld;
    public int indexMovement;
    public float offSet;
    public RectTransform rectTransform;
    public float animationSpeed;
    public AnimationCurve curve;
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
        print(indexMovement + toAdd);

        if(indexMovement + toAdd > 0 || indexMovement + toAdd < -2)
            return;

        indexMovement += toAdd;
        posToStartMove = rectTransform.anchoredPosition;


        lerpT = 0;
    }
}
