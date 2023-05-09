using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    //Variables
    public string interactKey = "e";
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactText;
    public GameObject player;
    public float fadeDuration = 2f; 
    public Image blackStarterScreen;
    public AudioSource audioSource; 
    public KeyPickup keyPickupScript;
    public GameObject P4Enemy;
    
    private bool playerInRange = false;
    private bool interactionDisabled = false;
    
    
    void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    //when the player enters the collider a interact text shows
    void OnTriggerEnter(Collider other)
    {
        if (!interactionDisabled && IsPlayerLayer(other.gameObject))
        {
            playerInRange = true;
            interactText.text = "Press E to open door";
            interactText.gameObject.SetActive(true);
        }
    }

    //when the player leaves the collider the interact text is disabled
    void OnTriggerExit(Collider other)
    {
        if (!interactionDisabled && IsPlayerLayer(other.gameObject))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false);
        }
    }

    //update checks if the player is inrange and is pressing the interactbutton and that the interaction isnt disabled
    //if it returns true TryOpenDoor is called
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey) && !interactionDisabled)
        {
            TryOpenDoor();
        }

    }

    public void TryOpenDoor()
    {
        //checks if the player has the key and disables the monster, starts the doorsound,
        //disables the interaction and the corutine starts. If they dont a text tells the player they dont have the key. 
        if (keyPickupScript.keyUIImage.gameObject.activeInHierarchy)
        {
            interactText.gameObject.SetActive(false);
            interactionDisabled = true;
            Debug.Log("player has won the game!");
            audioSource.Play();
            P4Enemy.SetActive(false);
        
            StartCoroutine(FadeOutCoroutine());

        }else
        {
            interactText.text = "You don't have the exit    key";
        }
        
    }

    bool IsPlayerLayer(GameObject obj)
    {
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }


    //Fades the screen to black and when its done it waits for 1f and sends the player to the main menu
    IEnumerator FadeOutCoroutine()
    {
        float startTime = Time.time;
        float alpha = 0f;
        blackStarterScreen.gameObject.SetActive(true); 

        // Loop until the alpha value reaches 1
        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeDuration);
            blackStarterScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        
        yield return new WaitForSeconds(1f); // Wait for 2 seconds after fade
        
        SceneManager.LoadScene("mainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}