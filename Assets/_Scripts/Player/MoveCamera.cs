using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    //Moves the camera to the new camera position. The camera follows the player
    void Update() {
        transform.position = cameraPosition.position;
    }
}
