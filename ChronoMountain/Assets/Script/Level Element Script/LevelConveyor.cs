using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Mwa.Chronomountain
{
    public class LevelConveyor : LevelElementBase
    {
        [SerializeField][Range(1, 4)] float speedFactor;
        [SerializeField] ScriptableDirection direction;
        // [SerializeField] Sprtie up;
        // [SerializeField] Sprtie left;
        // [SerializeField] Sprtie right;
        // [SerializeField] Sprtie down;

// #if UNITY_EDITOR
//         //! en fonction de direction j'applique potential un potential sprite different

// #endif

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    
        public override void OnStep(System.Action callBack)
        {
            //! dois set la target puis tween
            float distance = TileDistance.instance.DistanceWithNextSprite(direction, transform.position);
            Vector3 target = transform.position + (direction.GetDirection() * distance);

            DOTween.To((lerpT) =>
            {
                PlayerMovement.instance.transform.position = Vector3.Lerp(transform.position, target, lerpT);
            },
            0, 1, PlayerMovement.instance.speed * speedFactor).SetSpeedBased().OnComplete(() =>
            {
                if(callBack != null)
                    callBack();
            });
        }
    }
}
