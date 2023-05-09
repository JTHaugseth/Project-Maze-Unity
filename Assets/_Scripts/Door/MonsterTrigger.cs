using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    //Variables
    public GameObject monster;
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;

    private bool hasTriggered = false;
    private bool isMoving = false;
    private Collider triggerCollider;
    public GameObject phase1Monster;
    public GameObject phase4Monster;
    

    
    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    // checks if the monster is moving and
    void Update()
    {
        if (isMoving && Vector3.Distance(monster.transform.position, endPosition) > 0.1f)
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, endPosition, speed * Time.deltaTime);

            Vector3 direction = endPosition - monster.transform.position;

            monster.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else if (isMoving)
        {
            isMoving = false;
            monster.SetActive(false);
        }
    }

    //If the player enters the colider and no other monster is active, the monster moves from start to end with sound
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player") && !phase1Monster.activeSelf && !phase4Monster.activeSelf)
        {
            monster.SetActive(true);
            monster.transform.position = startPosition;
            isMoving = true;
            hasTriggered = true;
            triggerCollider.enabled = false;
            monster.GetComponent<AudioSource>().Play();
        }
    }
}