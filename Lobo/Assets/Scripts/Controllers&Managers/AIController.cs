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
    const float WAYPOINT_WIDTH = .3f;
    const int CHASE_RADIUS = 1;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        patrolRouteGenerator = GetComponent<PatrolRouteGenerator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.updateUpAxis = navMeshAgent.updateRotation = false;
        transform.eulerAngles = Vector3.zero;
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
        if (distanceToWaypoint > WAYPOINT_WIDTH) return;
        waypointIndex++;

        // If last waypoint is reached, restart patrol
        if (waypointIndex != waypointList.Count) return;
        waypointIndex = 0;
    }

    void AttackBehaviour()
    {
        // Check whether player is in range
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        var isNotInRangeOfPlayer = distanceToPlayer > CHASE_RADIUS;
        if (isNotInRangeOfPlayer) return;

        // Chase and shoot
        navMeshAgent.destination = player.transform.position;
        if (timeSinceLastShot < FIRE_RATE) return;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        timeSinceLastShot = 0;
    }
}
