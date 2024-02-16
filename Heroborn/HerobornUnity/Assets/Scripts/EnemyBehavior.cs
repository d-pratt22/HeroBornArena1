using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;

    public Transform patrolRoute;
   
    public List<Transform> locations;

    private int locationIndex = 0;

    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        // 2
        get { return _lives; }
        // 3
        private set
        {
            _lives = value;
            // 4
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;

        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }

    void Update()
    {
        // 1
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            // 2
            MoveToNextPatrolLocation();
        }
    }

        void InitializePatrolRoute()
    {
        
        foreach (Transform child in patrolRoute)
        {
           
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Enemy detected!");
        }
    }
    void OnTriggerExit(Collider other)
    {
 
    }
    void OnCollisionEnter(Collision collision)
    {
        // 5
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            // 6
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }


}
