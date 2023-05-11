using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class ResetProgressionLevel : MonoBehaviour
    {
        [SerializeField] List<ScriptableLevel> levelList;

        public void ResetIsWin()
        {
            foreach (var item in levelList)
            {
                item.SetIsWin(false);
            }
        }
    }
}
