using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Variables

    // Mouse sensitivity
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public PlayerHealth playerHealth;

    private bool canProcessInput = false;

    //Locks the cursor and makes it invisible
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize rotation values
        xRotation = WrapAngle(transform.eulerAngles.x);
        yRotation = orientation.eulerAngles.y;

        StartCoroutine(EnableInputProcessing());
    }

    // Coroutine to enable input processing after a delay
    IEnumerator EnableInputProcessing()
    {
        yield return new WaitForSeconds(0.5f);
        canProcessInput = true;
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale != 0f && playerHealth.currentHealth > 0 && canProcessInput)
        {
            // Get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.fixedDeltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.fixedDeltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rotate camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    // Method to wrap angles around 360 degrees
    float WrapAngle(float angle) {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }
}
