using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioReference : MonoBehaviour
{
    public void PlaySfx(AudioClip clip)
    {
        AudioManager.manager.PlaySfx(clip);
    }
}
