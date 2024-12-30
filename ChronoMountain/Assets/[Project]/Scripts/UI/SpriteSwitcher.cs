using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] Sprite initialeSprite;
    [SerializeField] Sprite spriteToSwitch;
    [SerializeField] SpriteRenderer spriteRendererToSet;
    [SerializeField] Image imageToSet;
    bool isInitialSprite = true;

    public void SwitchSprite()
    {
        if (imageToSet)
        {
            isInitialSprite = !isInitialSprite;
            if (isInitialSprite)
                imageToSet.sprite = initialeSprite;
            else
                imageToSet.sprite = spriteToSwitch;
        }

        if (spriteRendererToSet)
        {
            isInitialSprite = !isInitialSprite;
            if (isInitialSprite)
                spriteRendererToSet.sprite = initialeSprite;
            else
                spriteRendererToSet.sprite = spriteToSwitch;
        }
    }

    public void ResetSprite()
    {
        if (imageToSet)
            imageToSet.sprite = initialeSprite;

        if (spriteRendererToSet)
            spriteRendererToSet.sprite = initialeSprite;
    }
}
