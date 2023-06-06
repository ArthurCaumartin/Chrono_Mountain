using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mwa.Chronomountain
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance;

        void Awake()
        {
            instance = this;
        }

        public void LoadScene(string toLoad)
        {
            SceneManager.LoadScene(toLoad);
        }

        [ContextMenu("LoadNextLevel")]
        public void LoadNextLevel()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            int nextSceneIndex = Convert.ToInt32(sceneName.Split("_")[1]) + 1;

            string sceneToLoad = sceneName.Split("_")[0] + "_" + Convert.ToString(nextSceneIndex);
            // print("Next scene = " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
