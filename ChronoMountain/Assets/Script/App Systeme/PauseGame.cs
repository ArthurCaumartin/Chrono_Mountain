using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    //! Ce scripte dois gerer la mise en pause du jeu
    bool isPause = false;

    public void SwitchPauseState()
    {
        isPause = !isPause;
        if(isPause)
            Time.timeScale = 0;
        if(!isPause)
            Time.timeScale = 1;
    }
}
