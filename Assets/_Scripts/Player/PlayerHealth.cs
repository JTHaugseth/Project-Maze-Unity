using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image screenOverlay; // reference to the full-screen Image component
    public GameObject gameOverScreen;
    public GameObject gameOverText;
    public GameObject restart;
    public Button restartButton;

    private bool isGameOver = false;

    //sets the playerhealh to maxhealth
    //disables gameoverscreen
    private void Start()
    {
        currentHealth = maxHealth;
        gameOverScreen.SetActive(false);
        gameOverText.SetActive(false);
        restart.SetActive(false);
        restartButton.onClick.AddListener(Restart);
    }

    //When the player takes dammage it removes it from the player health and stops time and enables the gameoverscreen.
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0f; 
            gameOverScreen.SetActive(true);
            StartCoroutine(FadeToBlack());
        }
    }

    //when the player dies the screen turns red and then fades to black. In the end it shows the gameover text and restart button.
    private IEnumerator FadeToBlack()
    {
        Color startColor = screenOverlay.color;
        Color endColor = Color.black;
        float duration = 1f; 

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; 
            screenOverlay.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            yield return null;
        }

        gameOverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        restart.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //When the player presses the restart button time starts again and the scene is reloaded.
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}