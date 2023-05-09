using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerEnemyAI : MonoBehaviour
{   
    // Component-references
    public PlayerHealth playerHealth;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask obstacleMask;

    // Detection settings
    [Header("Detection")]
    public float detectionRange;
    public float patrolSpeed;
    public float chaseSpeed;
    private bool playerDetected = false;
    private Vector3 lastSeenPosition;

    // Attack settings
    [Header("Attack")]
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;
    private bool canAttack = true;

    // Patrol settings
    [Header("Patrol")]
    public List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;

    // Sets autoRepath to false and sets the enemies speed to patrol speed at Start. 
    private void Start()
    {
        agent.autoRepath = false; // Disable auto-repath
        agent.speed = patrolSpeed;
    }

    private void Update()
    {
        // If the crawler can see the player it sets detected as true, goes to chase speed, and sets destination the the players position.
        if (CanSeePlayer())
        {
            playerDetected = true;
            lastSeenPosition = player.position;
            agent.speed = chaseSpeed;
            agent.SetDestination(lastSeenPosition);
        }
        // else if the enemy sets detected to false, goes to patrol speed, finds closest patrol point and starts patrolling.
        else if (playerDetected && agent.remainingDistance <= agent.stoppingDistance)
        {
            playerDetected = false;
            agent.speed = patrolSpeed;
            FindClosestPatrolPoint();
            Patrol();
        }
        // Last check to see if the player is not detected. Crawler will then patrol normally. 
        else if (!playerDetected)
        {
            Patrol();
        }
        // Checks if the player is detected and close enough for an attack. 
        if (playerDetected && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (canAttack)
            {
                Attack();
            }
        }
        // Forloop to draw a debug.line. In your Scene editor you can see the crawlers pathing in a red line.
        // This is not visible in game-mode. 
        for (int i = 0; i < agent.path.corners.Length - 1; i++)
        {
            Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.red);
        }
    }

    private bool CanSeePlayer()
    {
        // Checks if the player is within detection range. 
        if (Vector3.Distance(transform.position, player.position) > detectionRange)
        {
            return false;
        }
        // // Calculates the direction to the player.
        Vector3 directionToPlayer = player.position - transform.position;
        // Shoots a raycast from the enemy's position to the direction it calculated and converts it to a "hit".
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
        {
            // Checks if the hit gameobject's layer is the same as the Player's layer (WhatIsPlayer).
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
            {
                return true;
            }
        }
        return false;
    }

    private void Patrol()
    {   
        // If the crawler has no set patrol points, he will stay still. 
        if (patrolPoints.Count == 0) return;

        // Checks if the remaining distance to the patrol point is less or equals to the agents pre-set stoppingdistance
        // If yes he will set currentPatrolIndex to current index + 1 modular of count, this is to stop him from going back to his original
        // patrol point and stop moving. 
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        // The crawler will after the checks set hit patrolpoint to his currentpatrolindex, and take its position as his path. 
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private void FindClosestPatrolPoint()
    {
        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        // Loops through the patrol points
        for (int i = 0; i < patrolPoints.Count; i++)
        {   
            // The crawler will check the distance to each patrol point.
            float distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            // If the calculated distance is less than the closestDistance, it will update closestDistance and closestIndex.
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        currentPatrolIndex = closestIndex;
    }

    private void Attack()
    {   
        // Checks if the crawler is within the attack range.
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {   
            // Performs attack if it is able to.
            if (canAttack)
            {
                playerHealth.TakeDamage(attackDamage);
                canAttack = false;
            }
        }
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
