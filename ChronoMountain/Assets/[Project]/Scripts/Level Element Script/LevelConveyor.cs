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

        // public bool flag;
        // public int i;
        Tween curretTween;

        // public Sprite up;
        // public Sprite left;
        // public Sprite right;
        // public Sprite down;

        void OnValidate()
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

    // [CustomEditor(typeof(LevelConveyor))]
    // public class LevelConveyorEditor : Editor
    // {
    //     public override void OnInspectorGUI()
    //     {
    //         DrawDefaultInspector();
    //         LevelConveyor levelConveyor = target as LevelConveyor;


    //         if(GUILayout.Button())
    //         // levelConveyor.flag = GUILayout.Toggle(levelConveyor.flag, "Flag");
    //         // if(levelConveyor.flag)
    //         //     levelConveyor.i = EditorGUILayout.FloatField("I field:", levelConveyor.i);
    //     }
    // }
}
