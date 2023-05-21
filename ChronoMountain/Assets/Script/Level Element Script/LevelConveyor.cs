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

        public Sprite up;
        public Sprite left;
        public Sprite right;
        public Sprite down;

        void OnValidate()
        {
            switch (direction.pointer)
            {
                case Pointer.Up :
                    GetComponent<SpriteRenderer>().sprite = up;
                break;
                case Pointer.Left :
                    GetComponent<SpriteRenderer>().sprite = left;
                break;
                case Pointer.Right :
                    GetComponent<SpriteRenderer>().sprite = right;
                break;
                case Pointer.Down :
                    GetComponent<SpriteRenderer>().sprite = down;
                break;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
        
        //TODO ajouter un enum pour le choix des directions
        public override void OnStep(System.Action callBack)
        {
            print(name + " OnStep Call");
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

    // [CustomEditor(typeof(LevelConveyor))]
    // public class LevelConveyorEditor : Editor
    // {
    //     public override void OnInspectorGUI()
    //     {
    //         LevelConveyor levelConveyor = target as LevelConveyor;

    //         levelConveyor.speedFactor = EditorGUILayout.FloatField("Speed Factor", levelConveyor.speedFactor);
    //         levelConveyor.direction = EditorGUILayout.ObjectField("Direction", levelConveyor.direction, ScriptableDirection, );


    //         levelConveyor.flag = GUILayout.Toggle(levelConveyor.flag, "Flag");
    //         if(levelConveyor.flag)
    //             levelConveyor.i = EditorGUILayout.FloatField("I field:", levelConveyor.i);
    //     }
    // }
}
