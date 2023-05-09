using UnityEngine;
using TMPro;

public class SwitchPartsCollector : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI collectedPartsText;
    public GameObject[] switchParts; 
    private int totalSwitchParts = 3;
    private int collectedSwitchParts = 0;
    private bool gameStarted = false; 

    private void Start()
    {
        // Hide the switch parts at the start of the game
        UpdateCollectedPartsText();
        collectedPartsText.gameObject.SetActive(false);
        foreach (GameObject part in switchParts) 
        {
            part.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        // Activate the switch parts when you have entered the middle
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0 && !gameStarted)
        {
            collectedPartsText.gameObject.SetActive(true);
            gameStarted = true;
            foreach (GameObject part in switchParts) 
            {
                part.SetActive(true);
            }
        }
    }

    public void CollectSwitchPart()
    {
        //Collect switch part
        collectedSwitchParts++;
        UpdateCollectedPartsText();
    }
        //Parts collected will update
    private void UpdateCollectedPartsText()
    {
        collectedPartsText.text = $"You have collected {collectedSwitchParts}/{totalSwitchParts} switch-parts";
    }
}