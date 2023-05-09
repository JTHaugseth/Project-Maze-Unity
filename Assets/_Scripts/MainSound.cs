using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    //variables
    public GameObject soundPlayer;
    public GameObject monster;
    public GameObject phase4Monster;
    public AudioClip mainAmbience;
    public AudioClip chaseMusic;
    public AudioClip phase4Music;
    

    void Start()
    {
    AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
    audioSource.clip = mainAmbience;
    audioSource.Play();
    audioSource.volume = 0.15f;
    }

    void Update()
{
    //checks if any monster is active or not and changes music
    AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();

    if (monster.activeSelf && audioSource.clip != chaseMusic)
    {
        audioSource.clip = chaseMusic;
        
        audioSource.Play();
        audioSource.volume = 0.1f;
    }
    else if (!monster.activeSelf  && audioSource.clip != mainAmbience && !phase4Monster.activeSelf)
    {
        audioSource.clip = mainAmbience;
        audioSource.Play();
        audioSource.volume = 0.15f;
    }
    else if(phase4Monster.activeSelf && audioSource.clip != phase4Music)
    {
        audioSource.clip = phase4Music;
        audioSource.Play();
        audioSource.volume = 0.15f;
    }
}
}
