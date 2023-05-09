using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlicker : MonoBehaviour
{
    // Settings for the torch flicker
    public Light torchLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2.0f;
    public float minRange = 5.0f;
    public float maxRange = 7.0f;
    public float flickerSpeed = 0.1f;

    private float t;

    void Start()
    {   
        if (torchLight == null)
        {
            torchLight = GetComponent<Light>();
        }
        // Start the flicker coroutine
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        // Continously change the intensity and range of the light to show the flickering effect.
        while (true)
        {
            t = Mathf.PingPong(Time.time * flickerSpeed, 1.0f);
            torchLight.intensity = Mathf.SmoothStep(minIntensity, maxIntensity, t);
            torchLight.range = Mathf.SmoothStep(minRange, maxRange, t);
            yield return null;
        }
    }
}