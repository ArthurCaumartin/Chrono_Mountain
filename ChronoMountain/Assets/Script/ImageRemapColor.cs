using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageRemapColor : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color oMin;
    [SerializeField] Color oMax;
    [SerializeField] AnimationCurve curve;

    void Update()
    {
        image.color = ColorUtils.RemapColor(image.fillAmount, oMin, oMax, curve);
    }
}
