using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
  
    // finds the slider and listen to changes
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(onValueChanged);
        
    }
    // when the slider moves the volume is ajusted
    private void onValueChanged(float volume)
    {
        audioSource.volume = volume;
    }

   
}
