using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "ScriptableCinematic", menuName = "ChronoMoutain/ScriptableCinematic")]
public class ScriptableCinematic : ScriptableObject
{
    [System.Serializable]
    public struct Data
    {
        public Sprite backGround;
        public string text;
        public float printDuration;
    }
    public List<Data> dataList;

    // public List<Sprite> backGroundSprite;
    // public List<string> textList;
}