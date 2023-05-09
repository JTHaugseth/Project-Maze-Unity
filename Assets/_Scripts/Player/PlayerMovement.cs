using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;
    
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    private float startYscale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip sprintingSound;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    //the player wil always have one movementstate asigned depending on their input
    public MovementState state;

    public enum MovementState{
        walking,
        sprinting,
        crouching,
        air
    }

    public PlayerHealth playerHealth;

    //Gets the rigid body component and freezes the rotation
    //sets playerheight and the player can jump
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYscale = transform.localScale.y;
    }

    
    private void Update() {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        MyInput();
        SpeedControl();
        StateHandler();

        // Handle drag when grounded
        if(grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    //Called moveplayer at fixed intervals to that the framerate doesnt dictate movement
    private void FixedUpdate() {
        MovePlayer();
    }

    // Gets playerInput and checks if the player can jump
    //also checks if the player is crouching and sets player height
    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    
        if(Input.GetKey(jumpKey) && readyToJump && grounded && playerHealth.currentHealth>0) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKeyDown(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f,ForceMode.Impulse);
            playerHeight = 2;
        }

        if(Input.GetKeyUp(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
            playerHeight = 2;
        }
        
    }

    //Checks if the player is grounded and player input to 
    // determine the player state
    private void StateHandler() {

    bool moving = horizontalInput != 0 || verticalInput != 0;
    bool wasSprinting = state == MovementState.sprinting;

    if (grounded && Input.GetKey(sprintKey) && !Input.GetKey(crouchKey)) {
        state = MovementState.sprinting;
        moveSpeed = sprintSpeed;
    } else if (grounded && Input.GetKey(crouchKey)) {
        state = MovementState.crouching;
        moveSpeed = crouchSpeed;
    } else if (grounded) {
        state = MovementState.walking;
        moveSpeed = walkSpeed;
    } else {
        state = MovementState.air;
    }

    // Handle crouching from sprinting
    if (wasSprinting && state == MovementState.crouching) {
        moveSpeed = crouchSpeed;
    }

    // Update state to walking if the player is not crouching or sprinting
    if (state == MovementState.crouching && !Input.GetKey(crouchKey)) {
        state = MovementState.walking;
        moveSpeed = walkSpeed;
    }

    // Handle sound transitions
    if (wasSprinting && state != MovementState.sprinting && audioSource.clip == sprintingSound) {
        audioSource.Stop();
    }

    if (state == MovementState.walking && moving && !audioSource.isPlaying) {
        audioSource.clip = walkingSound;
        audioSource.loop = true;
        audioSource.Play();
    } else if (state == MovementState.sprinting && moving && (!audioSource.isPlaying || audioSource.clip == walkingSound)) {
        audioSource.clip = sprintingSound;
        audioSource.loop = true;
        audioSource.Play();
    } else if ((!moving || state == MovementState.air) && audioSource.isPlaying) {
        audioSource.Stop();
    }
}
    
    private void MovePlayer() {

        //If the player is on a slope it mimmics gravity
        if(Onslope() && !exitingSlope){
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        if(playerHealth.currentHealth <= 0){
           return;
        }
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        // On Ground
        if(grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } 
        // In Air
        else if(!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //if the player is not on a slope it enables gravity again
        rb.useGravity = !Onslope();
        
    }

    //controls the player speed.
    private void SpeedControl() {

        //Sets the speed if player is on a slope
        if(Onslope() && !exitingSlope){
            if(rb.velocity.magnitude > moveSpeed){
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else{
          Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity
        if(flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }  
        }
        
    }

    //adds upwars force when Jump is called
    private void Jump() {

        exitingSlope = true;
        // Reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // When resetJump is called it lets the player jump and the player is no longer exitingSlope
    private void ResetJump() {
        readyToJump = true;

        exitingSlope = false;
    }

    //returns true if the player is standing on a slope with a certain angle
    private bool Onslope(){
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)){
           float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
           return angle < maxSlopeAngle && angle != 0; 
        }

        return false;
    }

    //returns the slope direction by projecting the movement direction onto the slope normal and normalizing the result.
    private Vector3 GetSlopeMoveDirection(){
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}   

