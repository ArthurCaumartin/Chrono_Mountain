using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mwa.Chronomountain
{
    [CreateAssetMenu(fileName = "ScriptableScene", menuName = "ChronoMoutain/ScriptableScene")]
    public class ScriptableScene : ScriptableObject
    {
        public string levelName;
        public bool isLevleWin;
    }
}
