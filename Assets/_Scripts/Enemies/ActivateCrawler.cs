using UnityEngine;
using TMPro;

public class ActivateCrawler : MonoBehaviour
{   
    // Component-references
    public GameObject enemy; // Assign the enemy GameObject in the Inspector
    public LayerMask WhatIsPlayer; // Configure the LayerMask in the Inspector
    private Collider myCollider;
    public TextMeshProUGUI hidePromt;

    // Sets myCollider as the object's collider.
    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {
            enemy.SetActive(true);
            hidePromt.gameObject.SetActive(true);
            myCollider.enabled = false;
        }
    }
}
