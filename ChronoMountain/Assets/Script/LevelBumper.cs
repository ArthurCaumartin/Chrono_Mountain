using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelBumper : LevelElementBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        print("bumper !");
    }
    public AnimationCurve curve = new AnimationCurve( new Keyframe[] { new Keyframe(0, 1), new Keyframe(.5f, 1.5f), new Keyframe(1, 1) } );
    public override void OnStep(System.Action callback)
    {
        print("should bump !");
        // if(callback != null) callback();
        
        DOTween.To((lerpT) =>
        {
            Mwa.Chronomountain.PlayerMovement.instance.transform.localScale =  curve.Evaluate(lerpT) * Vector3.one;           // transform.position = Vector3.Lerp(initialMovementPosition, target, lerpT);
        },
        0, 1, .5f).SetSpeedBased().SetEase(Ease.Linear).OnComplete( () => { if(callback != null) callback(); });
    }
}
