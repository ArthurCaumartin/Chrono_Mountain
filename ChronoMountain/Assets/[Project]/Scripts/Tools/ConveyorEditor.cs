using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Mwa.Chronomountain
{
    [CustomEditor(typeof(LevelConveyor))]
    public class ConveyorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Up"))
            {
                (target as LevelConveyor).direction = DirectionManager.instance.left;
                (target as LevelConveyor).OnValidate();
            }
            if(GUILayout.Button("Left"))
            {
                (target as LevelConveyor).direction = DirectionManager.instance.left;
                (target as LevelConveyor).OnValidate();
            }
            if(GUILayout.Button("Right"))
            {
                (target as LevelConveyor).direction = DirectionManager.instance.right;
                (target as LevelConveyor).OnValidate();
            }
            if(GUILayout.Button("Down"))
            {
                (target as LevelConveyor).direction = DirectionManager.instance.right;
                (target as LevelConveyor).OnValidate();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}