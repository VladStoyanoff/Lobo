using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    PatrolRouteGenerator patrolRouteGenerator;
    PlayerController player;
    NavMeshAgent navMeshAgent;

    [SerializeField] GameObject bulletPrefab;

    int waypointIndex = 0;
    float timeSinceLastShot = Mathf.Infinity;
    const int FIRE_RATE = 2;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        patrolRouteGenerator = GetComponent<PatrolRouteGenerator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.updateUpAxis = navMeshAgent.updateRotation = false;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        PatrolBehaviour();
        AttackBehaviour();
    }

    void PatrolBehaviour()
    {
        // Travel to current waypoint
        var waypointList = patrolRouteGenerator.GetWaypointsList();
        var waypointPosition = waypointList[waypointIndex].position;
        navMeshAgent.destination = waypointPosition;

        // Logic that decides when the waypoint should be incremented
        var distanceToWaypoint = Vector3.Distance(transform.position, waypointPosition);
        var waypointWidth = .3f;
        if (distanceToWaypoint > waypointWidth) return;
        waypointIndex++;

        // If last waypoints is reached, restart patrol
        if (waypointIndex != waypointList.Count) return;
        waypointIndex = 0;
    }

    void AttackBehaviour()
    {
        //Check whether player is in range
        var stopChasingVariable = 1;
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        var isNotInRangeOfPlayer = distanceToPlayer > stopChasingVariable;
        if (isNotInRangeOfPlayer) return;

        // Chase and shoot
        navMeshAgent.destination = player.transform.position;
        if (timeSinceLastShot < FIRE_RATE) return;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        timeSinceLastShot = 0;
    }
}
