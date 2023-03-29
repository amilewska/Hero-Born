using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private int lives = 3;
    public int enemyLives
    {
        get { return lives; }
        private set
        {
            lives = value;
            if (lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down");
            }
        }
    }
    private Transform player;
    [SerializeField]private Transform patrolRoute;
    [SerializeField] private List<Transform> locations;
    private int locationIndex = 0;
    private NavMeshAgent agent;

    private void Start()
    {
        //patrolRoute = GameObject.Find("PatrolRoute").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }
    private void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    private void MoveToNextPatrolLocation()
    {
        if(locations.Count == 0) return;
        
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
        

    }

    private void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player detected - ATTACT!");
            agent.destination = player.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol!");
            //MoveToNextPatrolLocation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            enemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }

}
