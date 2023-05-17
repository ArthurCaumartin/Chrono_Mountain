using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class LevelBumper : LevelElementBase
    {
        [SerializeField] Transform target;
        [SerializeField] float speed;
        [SerializeField] float rotationSpeed;
        [SerializeField] AnimationCurve curve;
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        public override void OnStep(System.Action callback)
        {
            Vector3 startPosition = PlayerMovement.instance.transform.position;
            DOTween.To((lerpT) =>
            {
                PlayerMovement.instance.transform.localScale =  curve.Evaluate(lerpT) * Vector3.one;
                PlayerMovement.instance.transform.Rotate(new Vector3(0, 0, rotationSpeed));
                PlayerMovement.instance.transform.position = Vector3.Lerp(startPosition, target.position, lerpT);
            },
            0, 1, speed).SetSpeedBased().SetEase(Ease.Linear)
            .OnComplete( () =>
            {
                if(callback != null)
                {
                    callback(); 
                }  
            });
        }
    }
}