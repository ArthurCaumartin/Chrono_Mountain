using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

namespace Mwa.Chronomountain
{
    public class LevelConveyor : LevelElementBase
    {
        public float speedFactor;
        public ScriptableDirection direction;
        Tween curretTween;

        public void OnValidate()
        {
            transform.rotation = direction.GetRotation() * Quaternion.Euler(new Vector3(0, 0, -90));
        }

        public override void KillTween()
        {
            curretTween.Kill();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
        
        //TODO ajouter un enum pour le choix des directions
        public override void OnStep(System.Action callBack)
        {
            PlayerMovement.instance.SetRotation(direction);
            // print(name + " OnStep Call");
            //! dois set la target puis tween
            float distance = TileDistance.instance.DistanceWithNextSprite(direction, transform.position);
            Vector3 target = transform.position + (direction.GetDirection() * distance);

            curretTween = DOTween.To((lerpT) =>
            {
                PlayerMovement.instance.transform.position = Vector3.Lerp(transform.position, target, lerpT);
            },
            0, 1, (Vector3.Distance(transform.position, target) * PlayerMovement.instance.speed) * speedFactor)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                if(callBack != null)
                    callBack();
            });
        }
    }
}
