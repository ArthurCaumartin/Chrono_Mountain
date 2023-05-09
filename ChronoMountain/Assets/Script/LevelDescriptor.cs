using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Mwa.Chronomountain
{
    
    public class LevelDescriptor : MonoBehaviour
    {
        [SerializeField] ScriptableLevel level;

        //! Call par le canvas manager depuis une liste de Level descriptor
        public bool IsLevelWin()
        {
            return level.isWin;
        }

        //! Load la scene
        public void LoadLevelScene()
        {
            SceneManager.LoadScene(level.levelSceneName);
        }
    }
}
