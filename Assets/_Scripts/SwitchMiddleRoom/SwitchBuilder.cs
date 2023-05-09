using UnityEngine;
using TMPro;

public class SwitchBuilder : MonoBehaviour
{
    public GameObject switchOutline;
    public GameObject realSwitch;
    public TextMeshProUGUI collectedPartsText;
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactionText;
    private bool switchBuilt = false;

    private void Start()
    {
        realSwitch.SetActive(false);
        interactionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        //show text when entering the middel
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if (collectedPartsText.text == "You have collected 3/3 switch-parts")
            {
                interactionText.text = "Press \"E\" to build switch";
            }
            else
            {
                interactionText.text = "You don't have the required parts";
            }
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //check if you can build the switch
        if (collectedPartsText.text == "You have collected 3/3 switch-parts" && Input.GetKeyDown(KeyCode.E) && switchOutline.activeSelf && !switchBuilt)
        {
            BuildSwitch();
            collectedPartsText.gameObject.SetActive(false);
            
        }

        if (switchBuilt && interactionText.gameObject.activeSelf)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
    //build switch
    private void BuildSwitch()
    {
        switchOutline.SetActive(false);
        realSwitch.SetActive(true);
        switchBuilt = true;
    }
}
