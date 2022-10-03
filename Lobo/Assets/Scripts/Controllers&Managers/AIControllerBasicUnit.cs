using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControllerBasicUnit : MonoBehaviour
{
    PatrolRouteGenerator patrolRouteGenerator;
    PlayerController player;
    NavMeshAgent navMeshAgent;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    int waypointIndex = 0;
    float timeSinceLastShot = Mathf.Infinity;

    const int FIRE_RATE = 2;
    const float WAYPOINT_WIDTH = .3f;
    const float BULLET_SPEED = 2f;
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

        var angle = Mathf.Atan2(waypointPosition.y - transform.position.y, waypointPosition.x - transform.position.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);

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
        if (player == null) return;
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        var isNotInRangeOfPlayer = distanceToPlayer > CHASE_RADIUS;
        if (isNotInRangeOfPlayer) return;

        // Chase and shoot
        if (timeSinceLastShot < FIRE_RATE) return;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bulletSpawnPoint.transform.localEulerAngles));
        bullet.tag = "Enemy Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * BULLET_SPEED;
        timeSinceLastShot = 0;

        // REFACTOR ^^ You can probably figure out a way to turn this into a general method and pass it to each controller
    }
}
