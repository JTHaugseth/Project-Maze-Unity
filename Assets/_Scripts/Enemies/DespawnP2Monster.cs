using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnP2Monster : MonoBehaviour
{
    // Component references
    public GameObject P2enemy;
    public LayerMask whatIsPlayer;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        if (((1 << other.gameObject.layer) & whatIsPlayer) != 0)
        {
            P2enemy.SetActive(false);
            boxCollider.enabled = false;
        }
    }
}
