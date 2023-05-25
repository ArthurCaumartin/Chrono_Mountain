using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableCinematic", menuName = "ChronoMoutain/ScriptableCinematic")]
public class ScriptableCinematic : ScriptableObject
{
    public Sprite backGround;
    public float timeToPrintText;
    public float animationTime;
    public string mainText;
}