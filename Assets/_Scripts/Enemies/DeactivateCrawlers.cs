using UnityEngine;

public class DeactivateCrawlers : MonoBehaviour
{
    // Creates an object-arraylist of enemies. 
    public GameObject[] enemies; 
    public LayerMask WhatIsPlayer; 

    // Checks when player walks into a collider.
    private void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {   
            // Loops through the array and disables them.
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false);
                }
            }
        }
    }
}
