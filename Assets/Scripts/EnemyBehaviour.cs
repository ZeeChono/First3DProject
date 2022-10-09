using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform PatrolRoute;
    [SerializeField] private List<Transform> Locations;
    [SerializeField] private Transform Player;
    
    private NavMeshAgent _agent;

    // numeric values
    private int _locationIndex = 0;
    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }

        set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down!");
            }
        }
    }

    

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if(_agent.remainingDistance < 0.2f && !_agent.pathPending)      // if the distance from destination is less than 0.2, goto next destination
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        foreach(Transform location in PatrolRoute)
        {
            Locations.Add(location);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if(Locations.Count == 0)
        {
            return;
        }
        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player detected - attack");
            _agent.destination = Player.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player out of range");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
    }
}
