using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] Sprite initialeSprite;
    [SerializeField] Sprite spriteToSwitch;
    [SerializeField] Image imageToSet;
    bool isInitialSprite = true;

    public void SwitchSprite()
    {
        isInitialSprite = !isInitialSprite;
        if(isInitialSprite)
            imageToSet.sprite = initialeSprite;
        else
            imageToSet.sprite = spriteToSwitch;
    }
}
