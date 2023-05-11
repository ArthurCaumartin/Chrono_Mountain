using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager manager;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        manager = this;
    }

    public void PlaySFX(AudioClip clip, float pitch)
    {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip);
    }
}
