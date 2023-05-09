using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class KeyPickup : MonoBehaviour
{
    public string interactKey = "e";
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactText;

    private KeyOutline keyOutline;
    private bool playerInRange = false;
    public Image keyUIImage;

    public AudioClip pickupSound;
    public GameObject audioPlayerPrefab; 

    void Start()
    {
        keyOutline = GetComponent<KeyOutline>();
        interactText.gameObject.SetActive(false);
    }

    //When an object enters the collider the keyoutline script will run it ShowOutline method. 
    void OnTriggerEnter(Collider other)
    {
        if (IsPlayerLayer(other.gameObject))
        {
            playerInRange = true;
            keyOutline.ShowOutline();
            interactText.gameObject.SetActive(true);
        }
    }

    // Does the opposite of the OnTriggerEnter.
    void OnTriggerExit(Collider other)
    {
        if (IsPlayerLayer(other.gameObject))
        {
            playerInRange = false;
            keyOutline.HideOutline();
            interactText.gameObject.SetActive(false);
        }
    }

    // Checks if the player has clicked the interactkey (E) when within the collider. 
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            PickUpKey();
        }
    }

    // disables the interact-text, enables key-image, plays pickup sound and destroys the key.
    void PickUpKey()
    {
        interactText.gameObject.SetActive(false);
        keyUIImage.gameObject.SetActive(true);
        PlaySound(pickupSound);
        Destroy(gameObject);
    }

    // Creates an instance of an empty object with a audiosource. (This is a prefab). 
    // Destroys the empty game object after the clip has stopped playing. 
    // This is done because the key is gone, and we wanted the sound to still play if the key was gone. 
    void PlaySound(AudioClip clip)
    {
        GameObject audioPlayer = Instantiate(audioPlayerPrefab);
        AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioPlayer, clip.length);
    }

    // 
    bool IsPlayerLayer(GameObject obj)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }
}