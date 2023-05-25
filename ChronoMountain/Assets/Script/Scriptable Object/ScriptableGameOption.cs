using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    [CreateAssetMenu(fileName = "ScriptableGameOption", menuName = "ChronoMoutain/ScriptableGameOption")]
    public class ScriptableGameOption : ScriptableObject
    {
        public bool isTimerOn;
    }
}
