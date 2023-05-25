using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableCinematic", menuName = "ChronoMoutain/ScriptableCinematic")]
public class ScriptableCinematic : ScriptableObject
{
    public Sprite backGround;
    public float duration;
    public string mainText;
}