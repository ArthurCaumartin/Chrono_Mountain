using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    [CreateAssetMenu(fileName = "ScriptableLevel", menuName = "ChronoMoutain/ScriptableLevel")]
    public class ScriptableLevel : ScriptableObject
    {
        public string levelSceneName;
        public bool isWin;

        public void SetIsWin(bool value)
        {
            isWin = value;
        }
    }
}
