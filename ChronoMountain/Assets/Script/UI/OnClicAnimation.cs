using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class OnClicAnimation : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] AnimationCurve curve;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(this.PlayAnimation);
    }
    
    void PlayAnimation()
    {
        print("Tween start !");
        DOTween.To((time) => 
        {
            transform.localScale = Vector3.one * curve.Evaluate(time);
        },
        0, 1, duration);
    }
}
