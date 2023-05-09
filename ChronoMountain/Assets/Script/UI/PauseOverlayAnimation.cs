using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayAnimation : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [SerializeField] float speed;
    [SerializeField] float newX;
    float lerpT = 0;
    float initialX;
    bool isOpen = false;
    RectTransform rectTransform;
    Vector2 newPose;
    Vector2 initialPos;

    void Awake()
    {
        rectTransform = (RectTransform)overlay.transform;
        initialX = rectTransform.anchoredPosition.x;

        newPose = new Vector2(newX, rectTransform.anchoredPosition.y);
        initialPos = new Vector2(initialX, rectTransform.anchoredPosition.y);
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
