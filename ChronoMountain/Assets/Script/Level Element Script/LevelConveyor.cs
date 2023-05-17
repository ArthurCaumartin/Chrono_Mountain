using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Mwa.Chronomountain
{
    public class LevelConveyor : LevelElementBase
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnStep(System.Action callBack)
        {
            DOTween.To((lerpT) =>{

            },
            0, 1, 5).OnComplete(() =>
            {
                if(callBack != null)
                    callBack();
            });
        }
    }
}
