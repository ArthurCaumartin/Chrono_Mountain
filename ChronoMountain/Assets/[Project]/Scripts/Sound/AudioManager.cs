using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;
    [Header("Music :")]
    [SerializeField] AudioSource musicAudioSource;

    [Header("Sfx :")]
    [SerializeField][Range(0, 2)] float pitchFactor;
    [SerializeField] GameObject audioSourceGameobject;
    bool isSfxPlayable = true;

    void Awake()
    {
        manager = this;
    }

    //! Joue un sfx et augment de pitch
    public void PlaySfxPitch(AudioClip clip, float pitch)
    {
        if(!isSfxPlayable)
            return;

        GameObject newObjectSource = Instantiate(audioSourceGameobject, transform.position, Quaternion.identity);
        AudioSource newSource = newObjectSource.GetComponent<AudioSource>();
        newSource.GetComponent<AudioSource>().pitch = pitch;
        newSource.PlayOneShot(clip);

        Destroy(newObjectSource, 5);
    }

    //! Joue un sfx
    public void PlaySfx(AudioClip clip)
    {
        if(!isSfxPlayable)
            return;

        GameObject newSource = Instantiate(audioSourceGameobject, transform.position, Quaternion.identity);
        newSource.GetComponent<AudioSource>().PlayOneShot(clip);
        Destroy(newSource, 5);
    }

    //! Turn on/off les sfx
    public void SwitchSfxOnOff()
    {
        isSfxPlayable = !isSfxPlayable;
    }

    //! Turn on/off la music
    public void SwitchMucisOnOff()
    {
        musicAudioSource.enabled = !musicAudioSource.enabled;
    }
}
