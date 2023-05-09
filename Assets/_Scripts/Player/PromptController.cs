using System.Collections;
using UnityEngine;
using TMPro;

public class PromptController : MonoBehaviour
{
    public float displayDuration = 1f;
    public float fadeInDuration = 3f;
    public float fadeOutDuration = 3f;

    private TextMeshProUGUI textMeshPro;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // When the text is enabled it checks and stops the fadeCourutione if its already playing, if not it sets fadecoroutine to the new 
    // StartCoroutine with the FadeInAndOut() method. 
    private void OnEnable()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
        fadeCoroutine = StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // The coroutine starts with a startColor and a endColor 
        float currentTime = 0f;
        Color startColor = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Through a while loop with current time and fadeInDuration the text fades from StartColor to endColor. 
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            textMeshPro.color = Color.Lerp(startColor, endColor, currentTime / fadeInDuration);
            yield return null;
        }

        // The coroutine waits for the fade in duration and display duration to complete. Then start it's fade out ending. 
        yield return new WaitForSeconds(displayDuration);

        // This is the exact opposite of the first while loop. The text will fade out. 
        currentTime = 0f;
        startColor = textMeshPro.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            textMeshPro.color = Color.Lerp(startColor, endColor, currentTime / fadeOutDuration);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
