using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayAnimation : MonoBehaviour
{
    public float newY;
    public float newX;

    bool isOpen = false;
    public void SwitchOverlayState()
    {
        isOpen = !isOpen;
    }
    
    void Update()
    {
        RectTransform rectTransform = (RectTransform)transform;
        rectTransform.anchoredPosition = new Vector2(newX, newY);

    }
}
