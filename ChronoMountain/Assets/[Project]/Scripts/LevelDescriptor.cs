using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mwa.Chronomountain
{
    public class LevelDescriptor : MonoBehaviour
    {
        [SerializeField] ScriptableLevel level;

        //! Call par le canvas manager depuis une liste de Level descriptor
        public bool IsLevelWin()
        {
            print($"Level : {level.name} is {PlayerPrefs.GetFloat(level.levelSceneName)}");
            return PlayerPrefs.GetInt(level.levelSceneName) == 1;
            // return level.isWin;
        }

        //! Load la scene
        public void LoadLevelScene()
        {
            SceneManager.LoadScene(level.levelSceneName);
        }
    }
}
