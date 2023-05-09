using System.Collections;
using UnityEngine;

public class CrawlerAudioTrigger : MonoBehaviour
{
    // Component-references
    public LayerMask whatIsPlayer;
    private BoxCollider boxCollider;
    private AudioSource audioSource;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        if (((1 << other.gameObject.layer) & whatIsPlayer) != 0)
        {
            StartCoroutine(PlayAudioAndDisableCollider());
        }
    }

    // The coroutine starts playing the audio, disables the collides, and stops when the audio is finished playing. 
    IEnumerator PlayAudioAndDisableCollider()
    {
        audioSource.Play();
        boxCollider.enabled = false;

        while (audioSource.isPlaying)
        {
            yield return null;
        }
    }
}