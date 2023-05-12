using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Mwa.Chronomountain
{
    public class SoundSfxManager : MonoBehaviour
    {
        public static SoundSfxManager manager;
        [SerializeField] AudioSource audioSource;
        [SerializeField] bool isSfxPlayable;

        void Awake()
        {
            manager = this;
        }

        public void PlaySFX(AudioClip clip)
        {
            if(!isSfxPlayable)
                return;
            audioSource.PlayOneShot(clip);
        }

        public void PlaySFXpitch(AudioClip clip, float pitch)
        {
            if(!isSfxPlayable)
                return;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip);
        }

        public void SwitchIsSfxPlayable()
        {
            isSfxPlayable = !isSfxPlayable;
        }
    }
}