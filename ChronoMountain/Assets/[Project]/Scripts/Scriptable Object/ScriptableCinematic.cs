using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableCinematic", menuName = "ChronoMoutain/ScriptableCinematic")]
public class ScriptableCinematic : ScriptableObject
{
    public Sprite backGroundSprite;
    public float timeToPrintText;
    public float backgroundMoveDuration;
    public string mainText;
}