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
        AssignPatrolAndCurrentWaypoint(out var waypointPosition, out var waypointList);
        navMeshAgent.destination = waypointPosition;

        var angle = Mathf.Atan2(waypointPosition.y - transform.position.y, waypointPosition.x - transform.position.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);

        StartApproachingNextWaypoint(waypointPosition);
        RestartPatrolRoute(waypointList);
    }

    void AttackBehaviour()
    {
        if (player == null) return;
        CheckIfPlayerIsInRange(out var isNotInRangeOfPlayer);
        if (isNotInRangeOfPlayer) return;
        Shoot(transform.right);
    }

    void AssignPatrolAndCurrentWaypoint(out Vector3 waypointPosition, out List<Transform> waypointList)
    {
        var list = patrolRouteGenerator.GetWaypointsList();
        var waypoint = list[waypointIndex].position;
        waypointList = list;
        waypointPosition = waypoint;
    }

    void StartApproachingNextWaypoint(Vector3 waypointPosition)
    {
        var distanceToWaypoint = Vector3.Distance(transform.position, waypointPosition);
        if (distanceToWaypoint > WAYPOINT_WIDTH) return;
        waypointIndex++;
    }

    void RestartPatrolRoute(List<Transform> waypointList)
    {
        if (waypointIndex != waypointList.Count) return;
        waypointIndex = 0;
    }

    void CheckIfPlayerIsInRange(out bool isNotInRangeOfPlayer)
    {
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        var check = distanceToPlayer > CHASE_RADIUS;
        isNotInRangeOfPlayer = check;
    }

    void Shoot(Vector3 bulletDirection)
    {
        // Chase and shoot
        if (timeSinceLastShot < FIRE_RATE) return;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bulletSpawnPoint.transform.localEulerAngles));
        bullet.tag = "Enemy Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * BULLET_SPEED;
        timeSinceLastShot = 0;
    }
}
