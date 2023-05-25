using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    bool isPause = false;

    void Awake()
    {
        Time.timeScale = 1;
    }

    public void SwitchPauseState()
    {
        isPause = !isPause;
        if(isPause)
            Time.timeScale = 0;
        if(!isPause)
            Time.timeScale = 1;
    }
}
