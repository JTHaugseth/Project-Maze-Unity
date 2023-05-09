using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFloating : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Moves the key up and down on the Y transform scale.
    void Update()
    {
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
