using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayAnimation : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [SerializeField] float speed;
    [SerializeField] float newY;
    float lerpT = 0;
    float initialY;
    bool isOpen = false;
    RectTransform rectTransform;
    Vector2 newPose;
    Vector2 initialPos;

    void Awake()
    {
        rectTransform = (RectTransform)overlay.transform;
        initialY = rectTransform.anchoredPosition.x;

        newPose = new Vector2(rectTransform.anchoredPosition.x, newY);
        initialPos = new Vector2(rectTransform.anchoredPosition.y, initialY);
    }

    //! call par button pause
    public void SwitchOverlayState()
    {
        isOpen = !isOpen;
    }
    
    void Update()
    {
        if(isOpen && lerpT < 1)
            lerpT += Time.unscaledDeltaTime * speed;

        if(!isOpen && lerpT > 0)
            lerpT += -Time.unscaledDeltaTime * speed;

        rectTransform.anchoredPosition = Vector2.Lerp(newPose, initialPos, lerpT);
    }
}