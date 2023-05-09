using UnityEngine;
using UnityEngine.UI;

public class ControlVoume  : MonoBehaviour
{
    // mucic
    public AudioSource music; 
    //enemy
    public AudioSource phase1Enemy;
    public AudioSource phase4Enemy;
    // player movment sounds
    public AudioSource player;
    public AudioSource torch;
    //interactions
    public AudioSource jailDoor;
    public AudioSource jailDoorRigth;
    public AudioSource knifeAudio;
    public AudioSource bookBigBrown;
    public AudioSource enteryDoor;
    public AudioSource hatch;
    public AudioSource pickUp;
    public AudioSource door1;
    public AudioSource doorEndOfPh3;
   // free spot if we need to add
    public AudioSource soundEffect14;
    public AudioSource soundEffect15;
    public AudioSource soundEffect16;
    
    // ui sliders
    public Slider musicSlider;
    public Slider audioSlider;
    
    //conect sliders to function
    private void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { OnmusicSliderValueChanged(); });
        audioSlider.onValueChanged.AddListener(delegate { OnaudioSliderValueChanged(); });
    }
    // adjust music volume
    private void OnmusicSliderValueChanged()
    {
        music.volume = musicSlider.value;
       
    }
    // adjust sound effect volume
    private void OnaudioSliderValueChanged()
    {
        
        jailDoor.volume = audioSlider.value;
        jailDoorRigth.volume = audioSlider.value;
        knifeAudio.volume = audioSlider.value;
        bookBigBrown.volume = audioSlider.value;
        player.volume = audioSlider.value;
        phase1Enemy.volume = audioSlider.value;
        phase4Enemy.volume = audioSlider.value;
        enteryDoor.volume = audioSlider.value;
        torch.volume = audioSlider.value;
        hatch.volume = audioSlider.value;
        pickUp.volume = audioSlider.value;
        door1.volume = audioSlider.value;
        doorEndOfPh3.volume = audioSlider.value;
    }
}
