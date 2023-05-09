using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchMovement : MonoBehaviour
{
    public Transform playerTransform;
    public PlayerMovement playerMovement;

    // Torch movement settings
    public float walkBobbingSpeed = 0.18f;
    public float sprintBobbingSpeed = 0.35f;
    public float verticalBobbingAmount = 0.1f;
    public float horizontalBobbingAmount = 0.05f;

    // Stores the initial position, and sets timer.
    private Vector3 initialPosition;
    private float timer = 0;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        // Activates the movement effect (bobbing) if the player is walking or sprinting. 
        if (playerMovement.state == PlayerMovement.MovementState.walking || playerMovement.state == PlayerMovement.MovementState.sprinting)
        {
            float bobbingSpeed = (playerMovement.state == PlayerMovement.MovementState.walking) ? walkBobbingSpeed : sprintBobbingSpeed;
            BobbingEffect(bobbingSpeed);
        }
        else
        {
            // If not its position and timer is reset
            timer = 0;
            transform.localPosition = initialPosition;
        }
    }

    private void BobbingEffect(float speed)
    {
        // Creates waveslices and get input from the player
        float waveSliceY = 0.0f;
        float waveSliceX = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Resets timer of there is no input from player
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            // Calculates waveslices and update the timer. If timer goes over 2PI it will reset back to the given range (0, 2PI). 
            waveSliceY = Mathf.Sin(timer);
            waveSliceX = Mathf.Cos(timer);
            timer = timer + speed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }

        // Applying the bobbing effect based on the waveslices. 
        if (waveSliceY != 0 || waveSliceX != 0)
        {
            float translateY = waveSliceY * verticalBobbingAmount;
            float translateX = waveSliceX * horizontalBobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateY = totalAxes * translateY;
            translateX = totalAxes * translateX;
            transform.localPosition = new Vector3(initialPosition.x + translateX, initialPosition.y + translateY, initialPosition.z);
        }
        else
        {
            // If the player isnt moving, reset the torch to its original position. 
            transform.localPosition = initialPosition;
        }
    }
}