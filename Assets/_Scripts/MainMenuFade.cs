using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFade : MonoBehaviour
{
    //Variables
    public float delayTime = 2f; 
    public float fadeDuration = 1f; 
    public Image blackStarterScreen; 

    void Start()
    {
        StartCoroutine(MainFade());
    }

    //When the game starts the screen fades from black to 0 opacity 
    IEnumerator MainFade()
    {
        yield return new WaitForSeconds(delayTime); 

        float startTime = Time.time;
        float alpha = 1f;

        // Loop until the alpha value reaches 0
        while (alpha > 0f)
        {
            alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeDuration);
            blackStarterScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        
        // Disable the image once the fade-in effect is complete
        gameObject.SetActive(false);
    }
}
