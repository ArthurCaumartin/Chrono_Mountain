using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mwa.Chronomountain
{
    public class LevelSelector : MonoBehaviour
    {
        public void LoadScene(ScriptableScene sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad.levelName);
        }
    }
}
