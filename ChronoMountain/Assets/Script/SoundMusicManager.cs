using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class SoundMusicManager : MonoBehaviour
    {
        public static SoundMusicManager manager;
        [SerializeField] AudioSource audioSource;
        // [SerializeField] AudioClip musicToPlay;
        bool isPause;

        void Awake()
        {
            manager = this;
        }
        
        void Start()
        {
            audioSource.Play();
        }

        public void SwitchPauseState()
        {
            isPause = !isPause;

            if(isPause)
                audioSource.UnPause();
            else
                audioSource.Pause();
        }
    }
}