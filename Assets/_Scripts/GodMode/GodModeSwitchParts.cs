using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GodModeSwitchParts : MonoBehaviour
{
    public GameObject player;  
    public GameObject part1; 
    public GameObject part2;  
    public GameObject part3;  
    public TextMeshProUGUI switchPartCollector;

    private bool spawnMode = false;
     // The distance in front of the player where the parts will spawn
    public float distance = 2f;  
    // The spacing between the parts
    public float spacing = 1f;  
   
    // The downward offset
    public float downOffsetPart1 = 0.5f;  
    public float downOffsetPart2 = 0.5f;  
    public float downOffsetPart3 = 0.5f;  

    void Update()
    {
        // Toggle spawn mode
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spawnMode = !spawnMode;  

            if (spawnMode)
            {
                SpawnParts();
            }
        }
    }

    void SpawnParts()
    {
        // make the parts activ
        switchPartCollector.gameObject.SetActive(true);
        part1.SetActive(true);
        part2.SetActive(true);
        part3.SetActive(true);

        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward;

        // Calculate the positions for the parts
        Vector3 part1Pos = playerPos + playerForward * distance + player.transform.right * -spacing;
        Vector3 part2Pos = playerPos + playerForward * distance;
        Vector3 part3Pos = playerPos + playerForward * distance + player.transform.right * spacing;

        // Apply the downward offset
        part1Pos.y -= downOffsetPart1;
        part2Pos.y -= downOffsetPart2;
        part3Pos.y -= downOffsetPart3;

        // Set positions of the parts
        part1.transform.position = part1Pos;
        part2.transform.position = part2Pos;
        part3.transform.position = part3Pos;
    }
}
