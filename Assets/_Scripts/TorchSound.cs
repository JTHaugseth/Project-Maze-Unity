using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSound : MonoBehaviour
{
    //Variables
    public AudioClip soundClip1;
    public AudioClip soundClip2;
    public float volume; 

    private AudioSource audioSource;

    void Start()
    {
        //Gets the audiocomponent for the torch
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClip1;
        audioSource.Play();
    }

    void Update()
    {
        //Checks if no audio is playing before it starts soundclip 2
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundClip2;
            audioSource.loop = true;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
