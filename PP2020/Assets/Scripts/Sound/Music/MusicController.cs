using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [NonSerialized]
    public AudioSource audioSource;

    public AudioClip acceleratingMusicAudioClip;
    public AudioClip constantMusicAudioClip;

    void Awake()
    {
        acceleratingMusicAudioClip = Resources.Load<AudioClip>("acceleratingMusic");
        constantMusicAudioClip = Resources.Load<AudioClip>("constantSpeedMusic");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = acceleratingMusicAudioClip;
        audioSource.loop = true;
        
    }

    void Start()
    {
        audioSource.Play();
    }

    public void SwitchToConstantSpeed()
    {
        audioSource.clip = constantMusicAudioClip;
        audioSource.Play();
    }

    public void SwitchToAccelerating()
    {
        audioSource.clip = acceleratingMusicAudioClip;
        audioSource.Play();
    }
}
