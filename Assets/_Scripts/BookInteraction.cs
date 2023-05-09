using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookInteraction : MonoBehaviour
{   
    //Variables
    public TextMeshProUGUI promptText;
    public Image bookImage;
    public LayerMask whatIsPlayer;
    public EntryMazeDoor door;
    public AudioSource bookAudioSource; 
    public AudioClip bookOpenSound; 
    public GameObject EntryDoorSound;
    public AudioClip OpenDoorSound; 

    private bool isPlayerInRange = false;
    private bool isBookOpen = false;
    private bool hasOpenedDoor = false;

    //sets prompt text to false and bookimage to false
    void Start()
    {
        promptText.gameObject.SetActive(false);
        bookImage.gameObject.SetActive(false);
    }

    //When the player enters the collider the text updates and is set to active
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            isPlayerInRange = true;
            UpdatePromptText();
            promptText.gameObject.SetActive(true);
        }
    }

    // When the player exits the collider the text is set to false and the image is set to false.
    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            isPlayerInRange = false;
            isBookOpen = false;
            promptText.gameObject.SetActive(false);
            bookImage.gameObject.SetActive(false);
        }
    }

    
    private void Update()
    {   
        //if player is in range and is pressing E the book image shows and the prompt text updates
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isBookOpen = !isBookOpen;
            bookImage.gameObject.SetActive(isBookOpen);
            UpdatePromptText();

            //If isbookOpen true the sound plays
            if (isBookOpen) 
            {
                bookAudioSource.clip = bookOpenSound;
                bookAudioSource.Play();
            }

            //if the door hasnt already opened and the book is open the door opens and door sound plays.
            if(!hasOpenedDoor && isBookOpen) {
                door.Open();
                hasOpenedDoor = true;
                EntryDoorSound.GetComponent<AudioSource>().clip = OpenDoorSound;
                EntryDoorSound.GetComponent<AudioSource>().Play();
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return ((1 << other.gameObject.layer) & whatIsPlayer) != 0;
    }

    // updates the prompt depending on if the book is open or not. 
    private void UpdatePromptText()
    {
        promptText.text = isBookOpen ? "Press \"E\" to close book" : "Press \"E\" to open book";
    }
}
