using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public string interactKey = "e";
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactText;
    public GameObject player;
    public KeyPickup keyPickupScript;
    public float rotationAngle = 90f;
    public float rotationSpeed = 2f;

    private bool playerInRange = false;
    private bool doorOpened = false;
    private bool interactionDisabled = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public AudioClip soundClip1;
    private AudioSource audioSource;
    public GameObject jailSound;
    public AudioClip soundClip2;

    void Start()
    {   
        interactText.gameObject.SetActive(false);
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) 
    {
        // When the player enters the zone of the door collider.
        // The function OnTriggerEnter is called.
        // If conditions for interaction between object and player is there,
        // Assigned message Press E will be displayed since playerInRange is True.
        if (!interactionDisabled && IsPlayerLayer(other.gameObject))
        {
            playerInRange = true;
            interactText.text = "Press E to open door";
            interactText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Like the comment above, this ones the same just the other way around
        // Changes playerInRange to False so the message disappear.
        if (!interactionDisabled && IsPlayerLayer(other.gameObject))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Once per frame this checks if the player is in range and pressed E interaction.
        // When it`s been opened, OpenDoor function is called.
        if (playerInRange && Input.GetKeyDown(interactKey) && !interactionDisabled)
        {
            TryOpenDoor();
        }

        if (doorOpened)
        {
            OpenDoor();
        }
    }

    public void TryOpenDoor()
    {
        if (keyPickupScript.keyUIImage.gameObject.activeInHierarchy)
        {
            // Set the keyUIImage to false to show the key is used.
            keyPickupScript.keyUIImage.gameObject.SetActive(false);

            audioSource.clip = soundClip1;
            audioSource.Play();

            // Wait for soundClip1 to finish before opening door
            Invoke("OpenDoorAfterSoundClip1", soundClip1.length);


            // Set interactText to false and disable further interactions
            interactText.gameObject.SetActive(false);
            interactionDisabled = true;
        }
        else
        {
            // If the player doesn't have a key, update the interactText.
            interactText.text = "You don't have the key";
        }
    }

    void OpenDoorAfterSoundClip1()
    {
        doorOpened = true;

    }

    void OpenDoor()
    {
    if (jailSound != null && jailSound.GetComponent<AudioSource>() != null)
        {
            AudioSource jailSoundAudio = jailSound.GetComponent<AudioSource>();

            // Play the jailSound clip
         if (!jailSoundAudio.isPlaying)
            {
                jailSoundAudio.clip = soundClip2;
                jailSoundAudio.Play();
                Destroy(jailSound, soundClip2.length);
            }
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
        
    }

    bool IsPlayerLayer(GameObject obj)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }
}
