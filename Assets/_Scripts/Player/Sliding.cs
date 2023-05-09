using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    //Variables
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYscale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;
    private bool sliding;


    //asigns variables to the rigidbody and playermovement
    private void Start(){
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYscale = playerObj.localScale.y;
    }

    //Checks if the player is pressing the slide key and  any movement key (wasd)
    private void Update(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horizontalInput !=0 || verticalInput !=0)){
            StartSlide();
        }
        if(Input.GetKeyUp(slideKey) && sliding){
            StopSlide();
        }
    }

    //updates the physics based movement
    private void FixedUpdate(){
        if(sliding){
            SlidingMovement();
        }
    }

    // changes the player height to slideYscale and adds downward force. Also sets the slideTimer.
    private void StartSlide(){
        sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    //Adds movement to the player depending on the players input. When the slidetimer reaches 0 it calls stopSlide
    private void SlidingMovement(){
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
        slideTimer -= Time.deltaTime;

        if(slideTimer <= 0){
            StopSlide();
        }
    }

    //Sets sliding to fals and sets player height back to normal
    private void StopSlide(){
        sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYscale, playerObj.localScale.z);
    }
    
}
