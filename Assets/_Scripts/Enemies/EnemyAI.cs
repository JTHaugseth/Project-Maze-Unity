using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Component-references
    public PlayerHealth playerHealth;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask obstacleMask;

    // Detection settings
    [Header("Detection")]
    public float initialSpeed;
    public float reducedSpeed;
    private bool playerDetected = false;

    // Attack settings
    [Header("Attack")]
    public float attackRange;
    public float attackDamage;
    private bool canAttack = true;

    // Active time settings
    [Header("Active Time")]
    public float activeTime = 30f;
    private Vector3 initialPosition;

    // Chase delay settings
    [Header("Chase Delay Timer")]
    public float fromSecond;
    public float toSecond;
    private bool chaseDelayComplete = false;

    // Audio settings
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip movingSound;
    public AudioClip JumpScare;
    public AudioClip screamSound;
    public float screamCooldown = 20f;
    private bool canScream = true;

    // Movement update (path finding) settings
    [Header("Movement Update Interval")]
    public float movementUpdateInterval = 0.1f;

    // The start method sets autoRepath in the nav mesh agent to false, stores the starting position of the enemy, 
    // and sets the enemy to false at the start of the game. 
    private void Start()
    {
        agent.autoRepath = false; // Disable auto-repath
        initialPosition = transform.position;
        gameObject.SetActive(false);
    }

    // When the enemy is enabled, het gets initial speed. Activetimer (How long he chases for) starts, and a chase delay ->
    // (how long he waits before chasing) starts. Audio begins and he is able to scream. 
    private void OnEnable()
    {
        agent.speed = initialSpeed;
        StartCoroutine(ActiveTimer());
        StartCoroutine(ChaseDelay());
        audioSource.clip = movingSound;
        audioSource.loop = true;
        audioSource.Play();
        canScream = true;
    }

    // This uses a coroutine to set the amount of seconds the enemy is active. 
    private IEnumerator ActiveTimer()
    {
        yield return new WaitForSeconds(activeTime);
        transform.position = initialPosition;
        gameObject.SetActive(false);
    }

    //Chasedelay uses 2 given variables to set a randomly timed coroutine before the enemy starts chasing
    private IEnumerator ChaseDelay()
    {
        float delay = Random.Range(fromSecond, toSecond);
        Debug.Log(delay);
        yield return new WaitForSeconds(delay);
        chaseDelayComplete = true;
        StartCoroutine(UpdateDestination());
    }

    // UpdateDestination creates a new NavMeshPath. It then calculates the path between the enemy's position, 
    // the players position, checks all nav mesh areas, and sets it to path.  
    private IEnumerator UpdateDestination()
    {
        while (true)
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, path))
            {
                agent.SetPath(path);
            }
            // If the enemy can see the player, it will have reduced speed /else not.
            if(CanSeePlayer()) {
                agent.speed = reducedSpeed;
            } else {
                playerDetected = false;
                agent.speed = initialSpeed;
            }

            yield return new WaitForSeconds(movementUpdateInterval);
        }
    }

    // Update does the following:    
    private void Update()
    {
    // 1. The first if checks if the chaseDelay coroutine is done, if not it doesnt run any more code in the update.
    if (!chaseDelayComplete) {
        return; 
    }    

    // 2. The second if checks if the enemy can scream, if yes, it does, and sets a cooldown on the scream. 
    // It also checks if the enemy is close enough to attack.
    if (playerDetected)
    {
        if(canScream) {
            audioSource.clip = screamSound;
            audioSource.loop = false;
            audioSource.Play(); 
            canScream = false;
            StartCoroutine(ScreamCooldownTimer());
        }
        // Check if we're close enough to attack
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (canAttack) {Attack();}
        }
    }

    // 3. The third if checks if the enemy's movement (magnitude) is greater than 0f (not moving) and
    // there is no sound playing. He will then start playing the movement sound again.
    if (agent.velocity.magnitude > 0f && !audioSource.isPlaying)
    {
        audioSource.clip = movingSound;
        audioSource.loop = true;
        audioSource.Play();
    }
    // 4. The else if checks if the enemy is not moving and the audiosource is playing. The audiosource will then stop.
    else if (agent.velocity.magnitude == 0f && audioSource.clip == movingSound)
    {
        audioSource.Stop();
    }
    }

    private bool CanSeePlayer()
    {
    // Calculates the direction to the player.
    Vector3 directionToPlayer = player.position - transform.position;
    
    // Shoots a raycast from the enemy's position to the direction it calculated and converts it to a "hit".
    if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit))
    {
        // Checks if the hit gameobject's layer is the same as the Player's layer (WhatIsPlayer).
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
        {
            // Sets playerDetected to true if the hit object was the player. 
            if (!playerDetected)
            {
                playerDetected = true;
            }
            return true;
        }
    }
    return false;
    }

    // Uses a coroutine to set a scream-cooldown. 
    private IEnumerator ScreamCooldownTimer()
    {
        yield return new WaitForSeconds(screamCooldown);
        canScream = true;
    }

    // The player takes damage, canAttack is set to false, and finishing sound is played. 
    private void Attack()
    {
        playerHealth.TakeDamage(attackDamage);
        Debug.Log("Attack!");
        canAttack = false;
            
        audioSource.clip = JumpScare;
        audioSource.loop = false;
        audioSource.Play();
    }
}


