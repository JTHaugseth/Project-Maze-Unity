using System.Collections;
using UnityEngine;

public class DoorCloseOnTrigger : MonoBehaviour
{
    public GameObject door;
    public float openAngle = -90.0f;
    public float closeAngle = 150.0f;
    public float doorCloseTime = 0.5f;
    public AudioClip doorCloseSound;

    private bool isOpen = false;
    private AudioSource audioSource;
    private Collider doorCollider;

    private void Start()
    {
        audioSource = door.GetComponent<AudioSource>();
        doorCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the gameobject has the same tag as the player. Player is set as "Player" in tags. 
        if (other.gameObject.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            StartCoroutine(RotateDoor(openAngle));
            doorCollider.enabled = false; 
        }
    }

    // Rotates the door from start to end position with given angle. 
    private IEnumerator RotateDoor(float angle)
    {
        float time = 0.0f;
        Quaternion startRotation = door.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0.0f, angle, 0.0f);

        // Play the door close sound
        if (audioSource && doorCloseSound)
        {
            audioSource.PlayOneShot(doorCloseSound);
        }

        while (time < doorCloseTime)
        {
            time += Time.deltaTime;
            door.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / doorCloseTime);
            yield return null;
        }

        
        isOpen = false;
    }
}