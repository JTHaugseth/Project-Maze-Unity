using UnityEngine;

public class ActivatePhase4Trigger : MonoBehaviour
{   
    // Component-references
    public GameObject objectToActivate; 
    public LayerMask WhatIsPlayer; 

    private void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {
            objectToActivate.SetActive(true);
        }
    }
}
