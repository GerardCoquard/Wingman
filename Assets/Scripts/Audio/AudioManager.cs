using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;

    public AudioSource SFXSource;
    public AudioSource AmbientSource;

    public AudioMixer AudioMixer;

    public PauseMenu pauseMenu;

    [Range(0, 1)]
    public float GlobalMusicVolume=1;
    [Range(0, 1)]
    public float GlobalSFXVolume=1;
    [Range(0, 1)]
    public float GlobalAmbientVolume = 1;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        Instance = this;
    }
    public static void PlaySFX(AudioSource audioSource)
    {
        Instance._PlaySFX(audioSource);
    }

    public static void PlayMusic(AudioSource audioSource)
    {
        Instance._PlayMusic(audioSource);
    }

    public static void StopMusic(AudioSource audioSource)
    {
        Instance._StopMusic(audioSource);
    }
    private  void _PlayMusic(AudioSource audioSource)
    {
        float vol = GlobalMusicVolume;
        audioSource.volume = vol;
        audioSource.Play();

    }

    private void _StopMusic(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    private void _PlaySFX(AudioSource audioSource)
    {
        float vol = GlobalSFXVolume;
        audioSource.volume = vol;
        audioSource.Play();
    }
}