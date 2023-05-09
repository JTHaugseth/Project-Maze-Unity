using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class pausegamemanager : MonoBehaviour
{
    public Button resumButton;
    public Button mainMenu;
    public Button retry;
    public static bool Paused = false;
    public GameObject PauseGamePanel;
    private bool CursorWasLocked = false;
    public PlayerHealth playerHealth;
   

    // finding all the buttons. unstop freeztime
    void Start()
    {
        resumButton.onClick.AddListener(Play);
        retry.onClick.AddListener(RestartGame);
        mainMenu.onClick.AddListener(MainMenuButton);
        Time.timeScale = 1f;

        
    }

    // update if you press the esc key to pause the game. if you are dead it will not run
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerHealth.currentHealth >0)
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
        if (playerHealth.currentHealth <=0)
        {
            PauseGamePanel.SetActive(false);
        }
    }
    // activet the pausecanvas and freezes time. locks the mouse curse to visible, mutes the game
    void Stop()
    {
        PauseGamePanel.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        CursorWasLocked = Cursor.lockState == CursorLockMode.Locked;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

       
            //mute
            AudioListener.pause = true;
    }
    // remove pausecanvas,unfreez the game. the mous curser disapair, audio unmuted.
    public void Play()
    {
        PauseGamePanel.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.visible = false;

        if (CursorWasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

       

        //unmute
        AudioListener.pause = false;

        Debug.Log("resumButton!!");
    }
    // function for the mainmenubutton to change scene back to main menu and unfreez the game
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        Paused = false;
        Debug.Log("back to menu");
    }
    // restartbutton reload lv 1 the player starts at the begining
    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Game is Restarted!!");
    }
}
